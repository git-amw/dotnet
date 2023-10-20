using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using verify.DataLayer.Data;
using verify.DataLayer.Interfaces;

namespace verify.DataLayer.Repo
{
    public class ValidationRepo : IValidation
    {
        private readonly AppDbContext db;

        public ValidationRepo(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> VerifyAadhaar(string aadhaarNumber)
        {
            var result = await db.AadhaarData.FirstOrDefaultAsync(a => a.AadhaarNumber == aadhaarNumber);
            if (result == null) return false;
            return true;
        }

        public async Task<bool> VerifyPAN(string pan)
        {
            var result = await db.PANData.FirstOrDefaultAsync(a => a.PAN == pan);
            if (result == null) return false;
            return true;
        }
    }
}
