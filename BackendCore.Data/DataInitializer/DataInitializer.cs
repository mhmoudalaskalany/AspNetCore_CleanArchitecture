using System.Collections.Generic;
using BackendCore.Common.Extensions;
using BackendCore.Entities.Entities;
using Permission = BackendCore.Entities.Entities.Permission;

namespace BackendCore.Data.DataInitializer
{
    public class DataInitializer : IDataInitializer
    {
        public Role[] SeedRoles()
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
        public User[] SeedUsers()
        {
            var userList = new List<User>();
            userList.AddRange(new[]
            {
                new User
                {
                    Id = 1,
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

        public Permission[] SeedPermissions()
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
    }
}