using System.ComponentModel.DataAnnotations;

namespace AMD.Services.Accounts.DomainLayer.DTOs
{
    public class RegistrationDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "PAN Should be of 10 Characters")]
        public string PAN { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Aadhaar Number Should be of 12 Characters")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{4})[-. ]?([0-9]{4})$", ErrorMessage = "Aadhaar number is not valid!")]
        public string Aadhaar { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phone number is not valid!")]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", ErrorMessage = "User email id format is not valid")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "User password format is not valid")]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "User password format is not valid")]
        public string ConfirmPassword { get; set; }


    }
}
