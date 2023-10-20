using System.ComponentModel.DataAnnotations;

namespace AMD.Services.Accounts.DomainLayer.DTOs
{
    public class LoginDTO
    {
        [Required]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", ErrorMessage = "User email id format is not correct")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "User password format is not correct")]
        public string Password { get; set; }
    }
}
