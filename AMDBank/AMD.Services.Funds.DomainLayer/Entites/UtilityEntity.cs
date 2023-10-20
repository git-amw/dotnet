using System.ComponentModel.DataAnnotations;

namespace AMD.Services.Funds.DomainLayer.Entites
{
    public class UtilityEntity
    {
        [Key]
        public int ID { get; set; }

        public int AccountNumber { get; set; }
    }
}
