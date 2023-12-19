using Microsoft.EntityFrameworkCore;
using ProniaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Persistence.Contexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get;}
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p=>p.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired().HasColumnType("decimal(6,2)");
            modelBuilder.Entity<Product>().Property(p => p.Description).IsRequired(false).HasColumnType("text");
            modelBuilder.Entity<Product>().Property(p => p.SKU).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Color>().Property(c=>c.Name).IsRequired().HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }
    }
}
