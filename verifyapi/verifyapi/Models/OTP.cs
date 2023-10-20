using System.ComponentModel.DataAnnotations;

namespace verifyapi.Models
{
    public class OTP
    {
        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Otp Code must be of 6 digits")]
        public string Code { get; set; }
       
    }

}
