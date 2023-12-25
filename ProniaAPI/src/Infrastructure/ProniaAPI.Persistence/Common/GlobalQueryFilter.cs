using Microsoft.EntityFrameworkCore;
using ProniaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Persistence.Common
{
    internal static class GlobalQueryFilter
    {
        public static void ApplyFilter<T>(this ModelBuilder modelBuilder) where T : BaseEntity, new()
        {
            modelBuilder.Entity<T>().HasQueryFilter(x => x.isDeleted == false);
        }
        public static void ApplyQueryFilters(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyFilter<Product>();
            modelBuilder.ApplyFilter<Category>();
            modelBuilder.ApplyFilter<Color>();
            modelBuilder.ApplyFilter<Tag>();
            modelBuilder.ApplyFilter<ProductColor>(); 
            modelBuilder.ApplyFilter<ProductTag>();
        }
    }
}
