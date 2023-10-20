using AMD.Services.Funds.DataLayer.Data;
using AMD.Services.Funds.DomainLayer.DTO;
using AMD.Services.Funds.DomainLayer.Entites;
using AMD.Services.Funds.DomainLayer.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMD.Services.Funds.DataLayer.Repository
{
    public class FundsRepo : IFunds
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        public FundsRepo(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<ReceiptEntity> CheckBalance(int accountNumber)
        {
            var accountDetails = await db.FundsTable.FirstOrDefaultAsync(u => u.AccountNumber == accountNumber);
            var result = new ReceiptEntity()
            {
                ReceiptType = "Available Balance",
                Amount = accountDetails.AccountBalance
            };
            return result;
        }

        public async Task<ReceiptEntity> DepositMoney(TransactionDTO transactionAmount, int accountNumber)
        {
            var accountDetails = await db.FundsTable.FirstOrDefaultAsync(u => u.AccountNumber == accountNumber);
            accountDetails.AccountBalance += transactionAmount.Amount;
            var transactionData = new TransactionHistoryEntity()
            {
                AccountNumber = accountNumber,
                Amount = transactionAmount.Amount,
                DateAndTime = System.DateTime.Now,
                TransactionType = "Deposit"
            };
            await db.TransactionHisotryTable.AddAsync(transactionData);
            await db.SaveChangesAsync();
            var result = new ReceiptEntity()
            {
                ReceiptType = "Deposit",
                Amount = transactionAmount.Amount
            };
            return result;
        }

        public async Task<List<TransactionHistoryReceiptEntity>> TransactionHistory(int accountNumber)
        {
            var transactionData = await db.TransactionHisotryTable.Where(u => u.AccountNumber == accountNumber).ToListAsync();
            var result = mapper.Map<List<TransactionHistoryReceiptEntity>>(transactionData);
            return result;
        }

        public async Task<ReceiptEntity> WithdrawMoney(TransactionDTO transactionAmount, int accountNumber)
        {
            var accountDetails = await db.FundsTable.FirstOrDefaultAsync(u => u.AccountNumber == accountNumber);
            var result = new ReceiptEntity()
            {
                ReceiptType = "Withdraw",
                Amount = -1
            };
            if (accountDetails.AccountBalance < transactionAmount.Amount)
            {
                return result;
            }
            accountDetails.AccountBalance -= transactionAmount.Amount;
            var transactionData = new TransactionHistoryEntity()
            {
                AccountNumber = accountNumber,
                Amount = transactionAmount.Amount,
                DateAndTime = System.DateTime.Now,
                TransactionType = "Withdraw"
            };
            await db.TransactionHisotryTable.AddAsync(transactionData);
            await db.SaveChangesAsync();
            result.Amount = transactionAmount.Amount;
            return result;
        }
    }
}
