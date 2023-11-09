using Microsoft.EntityFrameworkCore;
using Template.Common.Services;
using Template.Domain.Entities.Business;
using Template.Domain.Entities.Identity;
using Template.Infrastructure.Configuration;
using Template.Infrastructure.DataInitializer;
using Action = Template.Domain.Entities.Lookup.Action;
using Status = Template.Domain.Entities.Lookup.Status;

namespace Template.Infrastructure.Context
{
    public partial  class TemplateDbContext : DbContext
    {
        private readonly IDataInitializer _dataInitializer;
        private readonly IClaimService _claimService;
        public TemplateDbContext(DbContextOptions<TemplateDbContext> options, IDataInitializer dataInitializer, IClaimService claimService) : base(options)
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
            modelBuilder.Entity<Permission>().HasData(_dataInitializer.SeedPermissionsAsync());
            modelBuilder.Entity<Status>().HasData(_dataInitializer.SeedStatusesAsync());
            modelBuilder.Entity<Action>().HasData(_dataInitializer.SeedActionsAsync());

            base.OnModelCreating(modelBuilder);
        }
        



    }
}
