using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Domain.Entities.Identity;
using Template.Domain.Entities.Lookup;

namespace Template.Infrastructure.DataInitializer
{
    public interface IDataInitializer
    {
        IEnumerable<Role> SeedRoles();

        IEnumerable<User> SeedUsers();

        Task<IEnumerable<Permission>> SeedPermissionsAsync();

        Task<IEnumerable<Action>> SeedActionsAsync();

        Task<IEnumerable<Status>> SeedStatusesAsync();

    }
}