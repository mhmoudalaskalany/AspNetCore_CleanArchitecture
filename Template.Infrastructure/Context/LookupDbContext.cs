using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Lookup;

namespace Template.Infrastructure.Context
{
    public partial class BackendCoreDbContext
    {

        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<Action> Actions { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

    }
}
