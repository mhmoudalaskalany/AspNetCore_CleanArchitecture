using System.Collections.Generic;
using Template.Domain.Entities.Identity;
using Template.Domain.Entities.Lookup;

namespace Template.Infrastructure.DataInitializer
{
    public interface IDataInitializer
    {
        IEnumerable<Role> SeedRoles();

        IEnumerable<User> SeedUsers();

        IEnumerable<Permission> SeedPermissionsAsync();

        IEnumerable<Action> SeedActionsAsync();

        IEnumerable<Status> SeedStatusesAsync();

    }
}