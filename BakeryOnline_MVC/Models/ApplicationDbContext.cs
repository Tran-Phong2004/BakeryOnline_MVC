using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BakeryOnline_MVC.Models
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Model.GetEntityTypes().ToList().ForEach(entityType =>
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            });

        }
    }
}
