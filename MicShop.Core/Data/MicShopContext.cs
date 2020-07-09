using Microsoft.EntityFrameworkCore;
using MicShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicShop.Core.Data
{
    public class MicShopContext : DbContext
    {
        public MicShopContext(DbContextOptions<MicShopContext> options)
            : base(options)
        {
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryModel>()
                .HasMany(c => c.Products)
                .WithOne(e => e.Category);
            modelBuilder.Entity<UserModel>(etb =>
            {
                etb.HasKey(e => e.ID);
                etb.Property(e => e.ID).ValueGeneratedOnAdd();
                etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
                etb.ToTable("Users");
            }
      );

            modelBuilder.Entity<CredentialType>(etb =>
            {
                etb.HasKey(e => e.Id);
                etb.Property(e => e.Id).ValueGeneratedOnAdd();
                etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
                etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
                etb.ToTable("CredentialTypes");
            }
            );

            modelBuilder.Entity<Credential>(etb =>
            {
                etb.HasKey(e => e.Id);
                etb.Property(e => e.Id).ValueGeneratedOnAdd();
                etb.Property(e => e.Identifier).IsRequired().HasMaxLength(64);
                etb.Property(e => e.Secret).HasMaxLength(1024);
                etb.ToTable("Credentials");
            }
            );

            modelBuilder.Entity<Role>(etb =>
            {
                etb.HasKey(e => e.Id);
                etb.Property(e => e.Id).ValueGeneratedOnAdd();
                etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
                etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
                etb.ToTable("Roles");
            }
            );

            modelBuilder.Entity<UserRole>(etb =>
            {
                etb.HasKey(e => new { e.UserId, e.RoleId });
                etb.ToTable("UserRoles");
            }
            );

            modelBuilder.Entity<Permission>(etb =>
            {
                etb.HasKey(e => e.Id);
                etb.Property(e => e.Id).ValueGeneratedOnAdd();
                etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
                etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
                etb.ToTable("Permissions");
            }
            );

            modelBuilder.Entity<RolePermission>(etb =>
            {
                etb.HasKey(e => new { e.RoleId, e.PermissionId });
                etb.ToTable("RolePermissions");
            }
            );
           
        }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CredentialType> CredentialType { get; set; }
        public DbSet<Credential> Credential { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<ContactModel> Contact { get; set; }

        public DbSet<CartItemModel> CartItem { get; set; }

        public DbSet<CartModel> Cart { get; set; }

        public DbSet<OrderModel> Order { get; set; }
        public DbSet<MenuItemsModel> MenuItems { get; set; }
    }

}
