using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMD.Services.Accounts.DomainLayer.Entities
{
    public class RegistrationEntity: IdentityUser
    {
        public int AccountNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PAN { get; set; }

        public string Aadhaar { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }
    }
}
