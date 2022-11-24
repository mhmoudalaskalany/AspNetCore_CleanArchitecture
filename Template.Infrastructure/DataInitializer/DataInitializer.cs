using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Domain.Entities.Identity;
using Domain.Entities.Lookup;
using Action = Domain.Entities.Lookup.Action;
using Permission = Domain.Entities.Identity.Permission;

namespace Infrastructure.DataInitializer
{
    public class DataInitializer : IDataInitializer
    {
        #region Public Methods
        /// <summary>
        /// Seed Roles
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Seed Users
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Seed Permissions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Permission> SeedPermissions()
        {
            var permissionList = new List<Permission>();
            permissionList.AddRange(new[]
            {
                new Permission
                {
                    Id = 1,
                    NameEn = "Add",
                    NameAr = "اضافة",
                    Code = "Add",
                    CreatedDate = new DateTime(2021, 1, 1),
                    ModifiedDate = new DateTime(2021, 1, 1)
                },
                new Permission
                {
                    Id = 2,
                    NameEn = "Edit",
                    NameAr = "تعديل",
                    Code = "Edit",
                    CreatedDate = new DateTime(2021, 1, 1),
                    ModifiedDate = new DateTime(2021, 1, 1)
                },
                new Permission
                {
                    Id = 3,
                    NameEn = "View",
                    NameAr = "عرض",
                    Code = "View",
                    CreatedDate = new DateTime(2021, 1, 1),
                    ModifiedDate = new DateTime(2021, 1, 1)
                },
                new Permission
                {
                    Id = 4,
                    NameEn = "Delete",
                    NameAr = "حذف",
                    Code = "Delete",
                    CreatedDate = new DateTime(2021, 1, 1),
                    ModifiedDate = new DateTime(2021, 1, 1)
                }

            });

            return permissionList.ToArray();
        }
        /// <summary>
        /// Seed Actions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Action> SeedActions()
        {
            var enums = Enum.GetValues(typeof(Domain.Enum.Action));
            return (from object enumItem in enums
                select new Action
                {
                    Id = (int)(Domain.Enum.Action)enumItem,
                    NameEn = ((Domain.Enum.Action)enumItem).GetName().NameEn,
                    NameAr = ((Domain.Enum.Action)enumItem).GetName().NameAr,
                    Code = ((Domain.Enum.Action)enumItem).GetName().Code,
                    CreatedDate = new DateTime(2021, 1, 1),
                    ModifiedDate = new DateTime(2021, 1, 1)

                }).ToList();
        }
        /// <summary>
        /// Seed Statuses
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Status> SeedStatuses()
        {
            var enums = Enum.GetValues(typeof(Domain.Enum.Status));
            return (from object enumItem in enums
                select new Status
                {
                    Id = (int)(Domain.Enum.Status)enumItem,
                    NameEn = ((Domain.Enum.Status)enumItem).GetName().NameEn,
                    NameAr = ((Domain.Enum.Status)enumItem).GetName().NameAr,
                    Code = ((Domain.Enum.Status)enumItem).GetName().Code,
                    CreatedDate = new DateTime(2021, 1, 1),
                    ModifiedDate = new DateTime(2021, 1, 1)

                }).ToList();
        }

        #endregion

    }
}