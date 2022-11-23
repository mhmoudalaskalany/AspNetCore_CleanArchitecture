using System.Collections.Generic;
using Domain.Entities.Identity;
using Domain.Entities.Lookup;

namespace Infrastructure.DataInitializer
{
    public interface IDataInitializer
    {
        IEnumerable<Role> SeedRoles();
        IEnumerable<User> SeedUsers();
        IEnumerable<Permission> SeedPermissions();
        IEnumerable<Action> SeedActions();
        IEnumerable<Status> SeedStatuses();
    }
}