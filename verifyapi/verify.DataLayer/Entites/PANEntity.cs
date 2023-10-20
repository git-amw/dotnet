using System.ComponentModel.DataAnnotations;

namespace verify.DataLayer.Entites
{
    public class PANEntity
    {
        [Key]
        public int Id { get; set; }
        public string PAN { get; set; }
    }
}
