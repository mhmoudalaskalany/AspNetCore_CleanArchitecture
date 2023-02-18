using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Audit;
using Template.Domain.Enum;

namespace Template.Infrastructure.Context
{
    public partial class TemplateDbContext
    {

        public virtual DbSet<Audit> AuditTrails { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {

            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                foreach (var property in entry.Properties)
                {
                    var propertyName = property.Metadata.Name;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            if (propertyName == "CreatedById") property.CurrentValue = _claimService?.UserId;
                            else if (propertyName == "CreatedDate") property.CurrentValue = DateTime.Now;
                            break;
                        case EntityState.Modified:
                            if (propertyName == "ModifiedById") property.CurrentValue = _claimService?.UserId;
                            else if (propertyName == "ModifiedDate") property.CurrentValue = DateTime.Now;
                            break;
                    }
                }

            }
            OnBeforeSaveChanges();
            return (await base.SaveChangesAsync(true, cancellationToken));
        }


        private void OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                    continue;
                var userId = _claimService?.UserId;
                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Entity.GetType().Name,
                    UserId = userId,
                };


                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    var propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries)
            {
                AuditTrails.Add(auditEntry.ToAudit());
            }
        }
    }
}
