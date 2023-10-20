using AMD.Services.Funds.DomainLayer.Entites;
using Microsoft.EntityFrameworkCore;

namespace AMD.Services.Funds.DataLayer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<FundsEntity> FundsTable { get; set; }
        public DbSet<TransactionHistoryEntity> TransactionHisotryTable { get; set; }
    }
}
