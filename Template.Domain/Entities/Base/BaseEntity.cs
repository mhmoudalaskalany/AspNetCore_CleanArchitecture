using System;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.Base
{
    [ExcludeFromCodeCoverage]
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public string CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public string ModifiedById { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}