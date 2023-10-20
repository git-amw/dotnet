using System.ComponentModel.DataAnnotations;

namespace verify.DataLayer.Entites
{
    public class AadhaarEntity
    {
        [Key]
        public int Id { get; set; }
        public string AadhaarNumber { get; set; }
    }
}
