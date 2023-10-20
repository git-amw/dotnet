namespace AMD.Services.Funds.DomainLayer.Entites
{
    public class IncomingMessageEntity
    {
        public string WelcomeMessage { get; set; }

        public string BankIFSC { get; set; }

        public int YourAccountNumber { get; set; }

        public string UserEmail { get; set; }
    }
}
