using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int? SubscriptionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; }
        public int PaymentMethod { get; set; } // Cash, CreditCard, BankTransfer
        public int Status { get; set; } // Completed, Refunded, Pending
        public string ReferenceNo { get; set; }
        public string Notes { get; set; }
    }
}
