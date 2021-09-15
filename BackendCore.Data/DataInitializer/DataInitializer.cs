using System;
using System.Collections.Generic;
using System.Linq;
using BackendCore.Common.Extensions;
using BackendCore.Entities.Entities.Identity;
using BackendCore.Entities.Entities.Lookup;
using Action = BackendCore.Entities.Entities.Lookup.Action;
using Permission = BackendCore.Entities.Entities.Identity.Permission;

namespace BackendCore.Data.DataInitializer
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
                    Id = new Guid("abcc43c2-f7b8-4d70-8c1e-81bc61cb4518"),
                    NameEn = "Admin",
                    NameAr = "مدير",
                    Email = "Admin@admin.com",
                    Phone = "01016670280",
                    IsDeleted = false,
                    UserName = "admin",
                    Password = CryptoHasher.HashPassword("123456"),
                    RoleId = 1
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
                    Code = "Add"
                },
                new Permission
                {
                    Id = 2,
                    NameEn = "Edit",
                    NameAr = "تعديل",
                    Code = "Edit"
                },
                new Permission
                {
                    Id = 3,
                    NameEn = "View",
                    NameAr = "عرض",
                    Code = "View"
                },
                new Permission
                {
                    Id = 4,
                    NameEn = "Delete",
                    NameAr = "حذف",
                    Code = "Delete"
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
            var enums = Enum.GetValues(typeof(Entities.Enum.Action));
            return (from object enumItem in enums
                select new Action
                {
                    Id = (int)(Entities.Enum.Action)enumItem,
                    NameEn = ((Entities.Enum.Action)enumItem).GetName().NameEn,
                    NameAr = ((Entities.Enum.Action)enumItem).GetName().NameAr,
                    Code = ((Entities.Enum.Action)enumItem).GetName().Code,
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
            var enums = Enum.GetValues(typeof(Entities.Enum.Status));
            return (from object enumItem in enums
                select new Status
                {
                    Id = (int)(Entities.Enum.Status)enumItem,
                    NameEn = ((Entities.Enum.Status)enumItem).GetName().NameEn,
                    NameAr = ((Entities.Enum.Status)enumItem).GetName().NameAr,
                    Code = ((Entities.Enum.Status)enumItem).GetName().Code,
                    CreatedDate = new DateTime(2021, 1, 1),
                    ModifiedDate = new DateTime(2021, 1, 1)

                }).ToList();
        }

        #endregion

    }
}