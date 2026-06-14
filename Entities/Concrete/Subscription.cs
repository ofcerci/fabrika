using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class Subscription : IEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int PackageId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePaid { get; set; }
        public int Status { get; set; } // Active, Frozen, Expired, Cancelled
        public DateTime? FrozenStartDate { get; set; }
        public DateTime? FrozenEndDate { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
