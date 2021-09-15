using System.Collections.Generic;
using BackendCore.Entities.Entities.Identity;
using BackendCore.Entities.Entities.Lookup;

namespace BackendCore.Data.DataInitializer
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