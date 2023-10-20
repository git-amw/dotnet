using AMD.Services.Funds.DomainLayer.DTO;
using AMD.Services.Funds.DomainLayer.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMD.Services.Funds.DomainLayer.Interfaces
{
    public interface IFunds
    {
        Task<ReceiptEntity> DepositMoney(TransactionDTO transactionAmount, int accountNumber);

        Task<ReceiptEntity> WithdrawMoney(TransactionDTO transactionAmount, int accountNumber);

        Task<ReceiptEntity> CheckBalance(int accountNumber);

        Task<List<TransactionHistoryReceiptEntity>> TransactionHistory(int accountNumber);
    }
}
