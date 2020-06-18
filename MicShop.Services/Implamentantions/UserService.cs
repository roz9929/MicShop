using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicShop.Core.Data;
using MicShop.Core.Entities;
using MicShop.Core.Helpers;
using MicShop.Services.Helpers;
using MicShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MicShop.Services.Implamentantions
{
    public class UserService : IUserService
    {
        private readonly MicShopContext _context;
        public UserService(MicShopContext context)
        {
            _context = context;
        }

      

        public SignUpResult SignUp(UserModel user, string credentialTypeCode, string identifier)
        {
            return this.SignUp(user, credentialTypeCode, identifier, null);
        }

        public SignUpResult SignUp(UserModel user, string credentialTypeCode, string identifier, string secret)
        {
            
            _context.Users.Add(user);
            _context.SaveChanges();

            CredentialType credentialType = _context.CredentialType.FirstOrDefault(ct => ct.Code.ToLower() == credentialTypeCode.ToLower());

            if (credentialType == null)
                return new SignUpResult(success: false, error: SignUpResultError.CredentialTypeNotFound);

            Credential credential = new Credential();

            credential.UserId = user.ID;
            credential.CredentialTypeId = credentialType.Id;
            credential.Identifier = identifier;

            if (!string.IsNullOrEmpty(secret))
            {
                byte[] salt = Pbkdf2Hasher.GenerateRandomSalt();
                string hash = Pbkdf2Hasher.ComputeHash(secret, salt);

                credential.Secret = hash;
                credential.Extra = Convert.ToBase64String(salt);
            }

            _context.Credential.Add(credential);
            _context.SaveChanges();
            return new SignUpResult(user: user, success: true);
        }

        public void AddToRole(UserModel user, string roleCode)
        {
            Role role = _context.Roles.FirstOrDefault(r => r.Code.ToLower() == roleCode.ToLower());

            if (role == null)
                return;

            this.AddToRole(user, role);
        }

        public void AddToRole(UserModel user, Role role)
        {
            UserRole userRole = _context.UserRoles.Find(user.ID, role.Id);

            if (userRole != null)
                return;

            userRole = new UserRole();
            userRole.UserId = user.ID;
            userRole.RoleId = role.Id;
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
        }

        public void RemoveFromRole(UserModel user, string roleCode)
        {
            Role role = _context.Roles.FirstOrDefault(r => r.Code.ToLower() == roleCode.ToLower());

            if (role == null)
                return;

            this.RemoveFromRole(user, role);
        }

        public void RemoveFromRole(UserModel user, Role role)
        {
            UserRole userRole = _context.UserRoles.Find(user.ID, role.Id);

            if (userRole == null)
                return;

            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }

        public ChangeSecretResult ChangeSecret(string credentialTypeCode, string identifier, string secret)
        {
            CredentialType credentialType = _context.CredentialType.FirstOrDefault(ct => ct.Code.ToLower() == credentialTypeCode.ToLower());

            if (credentialType == null)
                return new ChangeSecretResult(success: false, error: ChangeSecretResultError.CredentialTypeNotFound);

            Credential credential = _context.Credential.FirstOrDefault(c => c.CredentialTypeId == credentialType.Id && c.Identifier == identifier);

            if (credential == null)
                return new ChangeSecretResult(success: false, error: ChangeSecretResultError.CredentialNotFound);

            byte[] salt = Pbkdf2Hasher.GenerateRandomSalt();
            string hash = Pbkdf2Hasher.ComputeHash(secret, salt);

            credential.Secret = hash;
            credential.Extra = Convert.ToBase64String(salt);
            _context.Credential.Update(credential);
            _context.SaveChanges();
            return new ChangeSecretResult(success: true);
        }

        public ValidateResult Validate(string credentialTypeCode, string identifier)
        {
            return Validate(credentialTypeCode, identifier, null);
        }

        public ValidateResult Validate(string credentialTypeCode, string identifier, string secret)
        {
            CredentialType credentialType = _context.CredentialType.FirstOrDefault(ct => ct.Code.ToLower() == credentialTypeCode.ToLower());

            if (credentialType == null)
                return new ValidateResult(success: false, error: ValidateResultError.CredentialTypeNotFound);

            Credential credential = _context.Credential.FirstOrDefault(c => c.CredentialTypeId == credentialType.Id && c.Identifier == identifier);

            if (credential == null)
                return new ValidateResult(success: false, error: ValidateResultError.CredentialNotFound);

            if (!string.IsNullOrEmpty(secret))
            {
                byte[] salt = Convert.FromBase64String(credential.Extra);
                string hash = Pbkdf2Hasher.ComputeHash(secret, salt);

                if (credential.Secret != hash)
                    return new ValidateResult(success: false, error: ValidateResultError.SecretNotValid);
            }

            return new ValidateResult(user: _context.Users.Find(credential.UserId), success: true);
        }

        public async Task SignIn(HttpContext httpContext, UserModel user, bool isPersistent = false)
        {
            ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = isPersistent }
            );
        }

        public async Task SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public int GetCurrentUserId(HttpContext httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return -1;

            Claim claim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return -1;

            int currentUserId;

            if (!int.TryParse(claim.Value, out currentUserId))
                return -1;

            return currentUserId;
        }

        public UserModel GetCurrentUser(HttpContext httpContext)
        {
            int currentUserId = this.GetCurrentUserId(httpContext);

            if (currentUserId == -1)
                return null;

            return _context.Users.Find(currentUserId);
        }

        private IEnumerable<Claim> GetUserClaims(UserModel user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.AddRange(this.GetUserRoleClaims(user));
            return claims;
        }

        private IEnumerable<Claim> GetUserRoleClaims(UserModel user)
        {
            List<Claim> claims = new List<Claim>();
            IEnumerable<int> roleIds = _context.UserRoles.Where(ur => ur.UserId == user.ID).Select(ur => ur.RoleId).ToList();

            if (roleIds != null)
            {
                foreach (int roleId in roleIds)
                {
                    Role role = _context.Roles.Find(roleId);

                    claims.Add(new Claim(ClaimTypes.Role, role.Code));
                    claims.AddRange(this.GetUserPermissionClaims(role));
                }
            }

            return claims;
        }

        private IEnumerable<Claim> GetUserPermissionClaims(Role role)
        {
            List<Claim> claims = new List<Claim>();
            IEnumerable<int> permissionIds = _context.RolePermissions.Where(rp => rp.RoleId == role.Id).Select(rp => rp.PermissionId).ToList();

            if (permissionIds != null)
            {
                foreach (int permissionId in permissionIds)
                {
                    Permission permission = _context.Permissions.Find(permissionId);

                    claims.Add(new Claim("Permission", permission.Code));
                }
            }

            return claims;
        }
    }
 
       


        //public async Task<UserModel> Authenticate(string userName, string password)
        //{
        //    UserModel user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userName);
        //    if (user == null)
        //        return null;
        //    var passVerified = PasswordHashing.VerifyPasswordHash(password, user.Password, user.PasswordSalt);
        //    //    UserModel user = await _context.User.FirstOrDefaultAsync(u => u.Email == userName && u.Password == password);
        //    if (passVerified)
        //    {
        //        user.Password = null;
        //        return user;
        //    }
        //    else return null;


        //}

        //public Task Authenticate(string userName)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IActionResult> Logout()
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<UserModel> Register(UserModel usermodel, string password)
        //{
        //    // validation



        //    byte[] passwordHash, passwordSalt;
        //    PasswordHashing.CreatePasswordHash(password, out passwordHash, out passwordSalt);

        //    usermodel.Password = passwordHash;
        //    usermodel.PasswordSalt = passwordSalt;

        //    await _context.Users.AddAsync(usermodel);
        //    await _context.SaveChangesAsync();

        //    return usermodel;

        //}
        //public bool UserModelExists(string Email)
        //{
        //    return _context.Users.Any(e => e.Email == Email);
        //}



        //public UserModel Validate(string loginTypeCode, string identifier, string secret)
        //{
        //    CredentialType credentialType = _context.CredentialType.FirstOrDefault(ct => string.Equals(ct.Code, loginTypeCode, StringComparison.OrdinalIgnoreCase));

        //    if (credentialType == null)
        //        return null;

        //    Credential credential = _context.Credentials.FirstOrDefault(
        //      c => c.CredentialTypeId == credentialType.Id && string.Equals(c.Identifier, identifier, StringComparison.OrdinalIgnoreCase) && c.Secret == MD5Hasher.ComputeHash(secret)
        //    );

        //    if (credential == null)
        //        return null;

        //    return _context.Users.Find(credential.UserId);
        //}
    }
