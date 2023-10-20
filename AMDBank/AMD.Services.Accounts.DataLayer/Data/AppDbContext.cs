using AMD.Services.Accounts.DomainLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AMD.Services.Accounts.DataLayer.Data
{
    public class AppDbContext : IdentityDbContext<RegistrationEntity>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        DbSet<RegistrationEntity> BankCustomer { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasSequence<int>("GenerateAccountNumber").StartsAt(500000).IncrementsBy(1);

            builder.Entity<RegistrationEntity>()
                .Property(bankCustomer => bankCustomer.AccountNumber)
                .HasDefaultValueSql("NEXT VALUE FOR GenerateAccountNumber");

        }
    }
}
