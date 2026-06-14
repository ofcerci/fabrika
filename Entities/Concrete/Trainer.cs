using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class Trainer : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime HireDate { get; set; }
        public bool Status { get; set; }
        public string Notes { get; set; }
    }
}
