using System;
using System.Diagnostics.CodeAnalysis;

namespace BackendCore.Entities.Entities.Base
{
    [ExcludeFromCodeCoverage]
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public Guid? ModifiedById { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}