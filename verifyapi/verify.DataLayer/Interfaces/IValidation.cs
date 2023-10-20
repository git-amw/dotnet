using System.Threading.Tasks;

namespace verify.DataLayer.Interfaces
{
    public interface IValidation
    {
        Task<bool> VerifyAadhaar(string aadhaarNumber);

        Task<bool> VerifyPAN(string pan);
    }
}
