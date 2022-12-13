using Domain.Entities.Business;
using Domain.Entities.Identity;
using Infrastructure.Configuration;
using Infrastructure.DataInitializer;
using Microsoft.EntityFrameworkCore;
using Template.Common.Services;
using Action = Domain.Entities.Lookup.Action;
using Status = Domain.Entities.Lookup.Status;

namespace Infrastructure.Context
{
    public partial  class BackendCoreDbContext : DbContext
    {
        private readonly IDataInitializer _dataInitializer;
        private readonly IClaimService _claimService;
        public BackendCoreDbContext(DbContextOptions<Infrastructure.Context.BackendCoreDbContext> options, IDataInitializer dataInitializer, IClaimService claimService) : base(options)
        {
            _dataInitializer = dataInitializer;
            _claimService = claimService;
        }


        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<File> Files { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        

            modelBuilder.ApplyConfiguration(new PermissionConfig());
            modelBuilder.ApplyConfiguration(new ActionConfig());
            modelBuilder.ApplyConfiguration(new StatusConfig());

            modelBuilder.Entity<Role>().HasData(_dataInitializer.SeedRoles());
            modelBuilder.Entity<User>().HasData(_dataInitializer.SeedUsers());
            modelBuilder.Entity<Permission>().HasData(_dataInitializer.SeedPermissions());
            modelBuilder.Entity<Status>().HasData(_dataInitializer.SeedStatuses());
            modelBuilder.Entity<Action>().HasData(_dataInitializer.SeedActions());

            base.OnModelCreating(modelBuilder);
        }
        



    }
}
