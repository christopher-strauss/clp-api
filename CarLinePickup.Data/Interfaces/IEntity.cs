using System;

namespace CarLinePickup.Data.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
