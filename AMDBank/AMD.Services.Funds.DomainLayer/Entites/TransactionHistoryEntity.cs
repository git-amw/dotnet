using System;

namespace AMD.Services.Funds.DomainLayer.Entites
{
    public class TransactionHistoryEntity : UtilityEntity
    {
       public int Amount { get; set; }

       public DateTime DateAndTime { get; set; }

       public string TransactionType { get; set; }
    }
}
