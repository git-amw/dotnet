namespace AMD.Services.Accounts.DomainLayer.Entities
{
    public class OnboardResponseEntity
    {
        public string WelcomeMessage { get; set; }
        
        public string BankIFSC { get; set; }

        public int YourAccountNumber { get; set; }

        public string UserEmail { get; set; }

    }
}
