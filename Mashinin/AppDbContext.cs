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

            modelBuilder.Entity<Transport>()
            .HasOne(t => t.Make)
            .WithMany(m => m.Transports)
            .HasForeignKey(t => t.MakeId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transport>()
            .HasOne(t => t.Model)
            .WithMany(m => m.Transports)
            .HasForeignKey(t => t.ModelId)
            .OnDelete(DeleteBehavior.Restrict);

        }


        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<NumberPlate> NumberPlates { get; set; }
        public DbSet<ExtractedCarDetail> ExtractedCarDetails { get; set; }
        public DbSet<ExtractedNumber> ExtractedNumbers { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<TransportImage> TransportImages { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartSpecification> PartSpecifications { get; set; }
        public DbSet<PartImage> PartImages { get; set; }
    }
}
