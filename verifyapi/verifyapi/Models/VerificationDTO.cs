using System.ComponentModel.DataAnnotations;

namespace verifyapi.Models
{
    public class VerificationDTO
    {
        [Required]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", ErrorMessage = "User email id format is not valid")]
        public string Email { get; set; }
    }
}
