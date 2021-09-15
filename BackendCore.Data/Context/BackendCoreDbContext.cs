using BackendCore.Data.Configuration;
using BackendCore.Data.DataInitializer;
using BackendCore.Entities.Entities.Business;
using BackendCore.Entities.Entities.Identity;
using BackendCore.Entities.Entities.Lookup;
using Microsoft.EntityFrameworkCore;

namespace BackendCore.Data.Context
{
    public class BackendCoreDbContext : DbContext
    {
        private readonly IDataInitializer _dataInitializer;
        public BackendCoreDbContext(DbContextOptions<BackendCoreDbContext> options, IDataInitializer dataInitializer) : base(options)
        {
            _dataInitializer = dataInitializer;
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

        #region Business

        public virtual DbSet<Attachment> Attachments { get; set; }

        #endregion


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
    }
}
