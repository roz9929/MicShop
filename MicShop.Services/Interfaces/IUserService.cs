using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace MicShop.Services.Interfaces
{
    public enum SignUpResultError
    {
        CredentialTypeNotFound
    }

    public class SignUpResult
    {
        public UserModel User { get; set; }
        public bool Success { get; set; }
        public SignUpResultError? Error { get; set; }

        public SignUpResult(UserModel user = null, bool success = false, SignUpResultError? error = null)
        {
            this.User = user;
            this.Success = success;
            this.Error = error;
        }
    }

    public enum ValidateResultError
    {
        CredentialTypeNotFound,
        CredentialNotFound,
        SecretNotValid
    }

    public class ValidateResult
    {
        public UserModel User { get; set; }
        public bool Success { get; set; }
        public ValidateResultError? Error { get; set; }

        public ValidateResult(UserModel user = null, bool success = false, ValidateResultError? error = null)
        {
            this.User = user;
            this.Success = success;
            this.Error = error;
        }
    }

    public enum ChangeSecretResultError
    {
        CredentialTypeNotFound,
        CredentialNotFound
    }

    public class ChangeSecretResult
    {
        public bool Success { get; set; }
        public ChangeSecretResultError? Error { get; set; }

        public ChangeSecretResult(bool success = false, ChangeSecretResultError? error = null)
        {
            this.Success = success;
            this.Error = error;
        }
    }
    public interface IUserService
    {
        //Task<UserModel> Authenticate(string userName, string password);
        //Task<UserModel> Register(UserModel usermodel,string password);
        //Task Authenticate(string userName);
        //Task<IActionResult> Logout();
        //bool UserModelExists(string Email);

        SignUpResult SignUp(UserModel user, string credentialTypeCode, string identifier);
        SignUpResult SignUp(UserModel user, string credentialTypeCode, string identifier, string secret);
        void AddToRole(UserModel user, string roleCode);
        void AddToRole(UserModel user, Role role);
        void RemoveFromRole(UserModel user, string roleCode);
        void RemoveFromRole(UserModel user, Role role);
        ChangeSecretResult ChangeSecret(string credentialTypeCode, string identifier, string secret);
        ValidateResult Validate(string credentialTypeCode, string identifier);
        ValidateResult Validate(string credentialTypeCode, string identifier, string secret);
        Task SignIn(HttpContext httpContext, UserModel user, bool isPersistent = false);
        Task SignOut(HttpContext httpContext);
        int GetCurrentUserId(HttpContext httpContext);
        UserModel GetCurrentUser(HttpContext httpContext);
    }
}
