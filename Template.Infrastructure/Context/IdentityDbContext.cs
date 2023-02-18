using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Identity;

namespace Template.Infrastructure.Context
{
    public partial class TemplateDbContextDbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

    }
}
