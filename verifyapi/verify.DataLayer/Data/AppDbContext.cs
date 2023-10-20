using Microsoft.EntityFrameworkCore;
using verify.DataLayer.Entites;

namespace verify.DataLayer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<AadhaarEntity> AadhaarData { get; set; }
        public DbSet<PANEntity> PANData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AadhaarEntity>().HasData(new AadhaarEntity
            {
                Id = 1,
                AadhaarNumber = "124692735693"
            });

            modelBuilder.Entity<AadhaarEntity>().HasData(new AadhaarEntity
            {
                Id = 2,
                AadhaarNumber = "865196029806"
            });

            modelBuilder.Entity<AadhaarEntity>().HasData(new AadhaarEntity
            {
                Id = 3,
                AadhaarNumber = "378926190808"
            });

            modelBuilder.Entity<AadhaarEntity>().HasData(new AadhaarEntity
            {
                Id = 4,
                AadhaarNumber = "944879507126"
            });

            modelBuilder.Entity<AadhaarEntity>().HasData(new AadhaarEntity
            {
                Id = 5,
                AadhaarNumber = "364866624600"
            });

            modelBuilder.Entity<PANEntity>().HasData(new PANEntity
            {
                Id = 1,
                PAN = "1343CA3FEC"
            });

            modelBuilder.Entity<PANEntity>().HasData(new PANEntity
            {
                Id = 2,
                PAN = "23CECC43EA"
            });

            modelBuilder.Entity<PANEntity>().HasData(new PANEntity
            {
                Id = 3,
                PAN = "FECD3D44FD"
            });

            modelBuilder.Entity<PANEntity>().HasData(new PANEntity
            {
                Id = 4,
                PAN = "AA4D1C1DAE"
            });

            modelBuilder.Entity<PANEntity>().HasData(new PANEntity
            {
                Id = 5,
                PAN = "24EB4AD3D2"
            });
        }
    }
}
