using System;
using BackendCore.Entities.Entities.Base;

namespace BackendCore.Entities.Entities.Audit
{
    public class Audit : BaseEntity<Guid>
    {
        public string UserId { get; set; }
        public string Type { get; set; }
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumns { get; set; }
        public string PrimaryKey { get; set; }
    }
}
