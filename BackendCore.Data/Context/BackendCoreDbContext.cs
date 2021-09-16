using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BackendCore.Common.Services;
using BackendCore.Data.Configuration;
using BackendCore.Data.DataInitializer;
using BackendCore.Entities.Entities.Audit;
using BackendCore.Entities.Entities.Business;
using BackendCore.Entities.Entities.Identity;
using BackendCore.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using Action = BackendCore.Entities.Entities.Lookup.Action;
using Status = BackendCore.Entities.Entities.Lookup.Status;

namespace BackendCore.Data.Context
{
    public class BackendCoreDbContext : DbContext
    {
        private readonly IDataInitializer _dataInitializer;
        private readonly IClaimService _claimService;
        public BackendCoreDbContext(DbContextOptions<BackendCoreDbContext> options, IDataInitializer dataInitializer, IClaimService claimService) : base(options)
        {
            _dataInitializer = dataInitializer;
            _claimService = claimService;
        }

        #region Identity

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        #endregion



        #region Lookup

        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Action> Actions { get; set; }

        #endregion

        #region Audit

        public virtual DbSet<Audit> AuditTrails { get; set; }

        #endregion

        #region Business

        public virtual DbSet<Attachment> Attachments { get; set; }

        #endregion

        #region Overriden Methods
        /// <summary>
        /// On Model Creating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Configuration

            modelBuilder.ApplyConfiguration(new PermissionConfig());

            #endregion

            #region Seed

            modelBuilder.Entity<Role>().HasData(_dataInitializer.SeedRoles());
            modelBuilder.Entity<User>().HasData(_dataInitializer.SeedUsers());
            modelBuilder.Entity<Permission>().HasData(_dataInitializer.SeedPermissions());
            modelBuilder.Entity<Status>().HasData(_dataInitializer.SeedStatuses());
            modelBuilder.Entity<Action>().HasData(_dataInitializer.SeedActions());
            #endregion
            base.OnModelCreating(modelBuilder);
        }
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
