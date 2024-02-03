using Mashinin.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mashinin
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<BaseEntity>();
        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }

    }
}
