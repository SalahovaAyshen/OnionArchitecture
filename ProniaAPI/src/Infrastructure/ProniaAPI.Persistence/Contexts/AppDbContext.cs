using Microsoft.EntityFrameworkCore;
using ProniaAPI.Domain.Entities;
using ProniaAPI.Persistence.Common;
using System.Reflection;


namespace ProniaAPI.Persistence.Contexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get;}
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyQueryFilters();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entitites = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entitites)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = DateTime.Now;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;
                   
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
