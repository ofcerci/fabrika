using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class Attendance : IEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string Note { get; set; }
    }
}
