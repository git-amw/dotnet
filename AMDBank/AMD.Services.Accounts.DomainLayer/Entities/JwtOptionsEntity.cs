namespace AMD.Services.Accounts.DomainLayer.Entities
{
    public class JwtOptionsEntity
    {
        public string Issuer { get; set; } 

        public string Audience { get; set; } 

        public string Secret { get; set; } 
    }
}
