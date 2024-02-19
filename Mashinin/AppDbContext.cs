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
        public DbSet<City> Cities { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<NumberPlate> NumberPlates { get; set; }
        public DbSet<ExtractedCarDetail> ExtractedCarDetails { get; set; }
        public DbSet<ExtractedNumber> ExtractedNumbers{ get; set; }


    }
}
