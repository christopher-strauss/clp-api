using System;
using CarLinePickup.Domain.Models.Interfaces;

namespace CarLinePickup.Domain.Models
{
    public class ModelBase : IDomainModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
