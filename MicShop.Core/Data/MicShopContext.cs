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

        public DbSet<CategoryModel> Category { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryModel>()
                .HasMany(c => c.Products)
                .WithOne(e => e.Category);
        }
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<UserModel> User { get; set; }
    }

}
