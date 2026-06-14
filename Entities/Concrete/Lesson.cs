using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class Lesson : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TrainerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
    }
}
