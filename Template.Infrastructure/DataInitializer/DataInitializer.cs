using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Common.Extensions;
using Template.Domain.Entities.Identity;
using Template.Domain.Entities.Lookup;
using Action = Template.Domain.Entities.Lookup.Action;
using Permission = Template.Domain.Entities.Identity.Permission;

namespace Template.Infrastructure.DataInitializer
{
    public class DataInitializer : IDataInitializer
    {

        public IEnumerable<Role> SeedRoles()
        {
            var roleList = new List<Role>();
            roleList.AddRange(new[]
            {
                new Role
                {
                    Id = 1,
                    NameEn = "Admin",
                    NameAr = "مدير",
                    CreatedDate = new DateTime(2021, 1, 1),
                    ModifiedDate = new DateTime(2021, 1, 1)
                }

            });

            return roleList.ToArray();
        }

        public IEnumerable<User> SeedUsers()
        {
            var userList = new List<User>();
            userList.AddRange(new[]
            {
                new User
                {
                    Id =  Guid.Parse("abcc43c2-f7b8-4d70-8c1e-81bc61cb4518"),
                    NameEn = "Admin",
                    NameAr = "مدير",
                    Email = "Admin@admin.com",
                    Phone = "01016670280",
                    IsDeleted = false,
                    UserName = "admin",
                    Password = CryptoHasher.HashPassword("123456"),
                    RoleId = 1,
                    CreatedDate = new DateTime(2021, 1, 1),
                    ModifiedDate = new DateTime(2021, 1, 1)
                }

            });

            return userList.ToArray();
        }


        public async Task<IEnumerable<Permission>> SeedPermissionsAsync()
        {
            var dataText = await System.IO.File.ReadAllTextAsync(@"Seed/Permissions.json");
            var permissions = Seeder<List<Permission>>.SeedIt(dataText);
            return permissions;
        }

        public async Task<IEnumerable<Permission>> SeedActionsAsync()
        {
            var dataText = await System.IO.File.ReadAllTextAsync(@"Seed/Actions.json");
            var permissions = Seeder<List<Permission>>.SeedIt(dataText);
            return permissions;
        }

        public async Task<IEnumerable<Permission>> SeedStatusesAsync()
        {
            var dataText = await System.IO.File.ReadAllTextAsync(@"Seed/Statuses.json");
            var permissions = Seeder<List<Permission>>.SeedIt(dataText);
            return permissions;
        }

    }
}