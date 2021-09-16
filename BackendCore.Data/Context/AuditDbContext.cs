﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BackendCore.Entities.Entities.Audit;
using BackendCore.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace BackendCore.Data.Context
{
    public partial class BackendCoreDbContext
    {
        #region Audit Entities
        public virtual DbSet<Audit> AuditTrails { get; set; }
        #endregion


        #region Overriden Methods

        /// <summary>
        /// Save Changes
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Log Audit Trails Before Saving
        /// </summary>
        private void OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                    continue;
                var userId = _claimService?.UserId.ToString();
                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Entity.GetType().Name,
                    UserId = userId,
                };


                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
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

        #endregion
    }
}
