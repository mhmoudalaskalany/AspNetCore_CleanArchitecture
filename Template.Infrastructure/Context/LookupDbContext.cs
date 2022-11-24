using Domain.Entities.Lookup;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public partial class BackendCoreDbContext
    {

        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Action> Actions { get; set; }

    }
}
