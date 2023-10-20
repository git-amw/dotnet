using System.Threading.Tasks;

namespace AMD.Services.Funds.DomainLayer.Interfaces
{
    public interface IAlert
    {
        Task DepositAlert(string useremail, int amount, int userAccountNumber);

        Task WithdrawAlert(string useremail, int amount, int userAccountNumber);
    }
}
