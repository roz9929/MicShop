using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MicShop.Models;

namespace MicShop.Data
{
    public class MicShopContext : DbContext
    {
        public MicShopContext (DbContextOptions<MicShopContext> options)
            : base(options)
        {
        }

        public DbSet<MicShop.Models.CategoryModel> Category{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryModel>()
                .HasMany(c => c.Products)
                .WithOne(e => e.Category);
        }
        public DbSet<MicShop.Models.ProductModel> Product { get; set; }
    }
}
