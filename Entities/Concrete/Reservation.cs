using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class Reservation : IEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int LessonId { get; set; }
        public DateTime ReservedAt { get; set; }
        public int Status { get; set; } // Pending, Confirmed, Cancelled, NoShow
    }
}
