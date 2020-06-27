using System;

namespace BackendCore.Entities.Entities.Base
{
    public class BaseEntity
    {
        public long? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public long? ModifiedById { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}