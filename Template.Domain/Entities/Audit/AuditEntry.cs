using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Template.Domain.Enum;

namespace Template.Domain.Entities.Audit
{
    [ExcludeFromCodeCoverage]
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }
        public EntityEntry Entry { get; }

        public string UserId { get; set; }

        public string TableName { get; set; }

        public Dictionary<string, object> KeyValues { get; } = new();

        public Dictionary<string, object> OldValues { get; } = new();

        public Dictionary<string, object> NewValues { get; } = new();

        public AuditType AuditType { get; set; }

        public List<string> ChangedColumns { get; } = new();

        public Audit ToAudit()
        {
            var audit = new Audit
            {
                UserId = UserId,
                Type = AuditType.ToString(),
                TableName = TableName,
                DateTime = DateTime.UtcNow,
                PrimaryKey = JsonConvert.SerializeObject(KeyValues),
                OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues),
                NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues),
                AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns),
                CreatedById = UserId,
                ModifiedById = UserId
            };
            return audit;
        }
    }
}
