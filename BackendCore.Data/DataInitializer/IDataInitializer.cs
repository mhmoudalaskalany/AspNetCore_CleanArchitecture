using BackendCore.Entities.Entities;

namespace BackendCore.Data.DataInitializer
{
    public interface IDataInitializer
    {
        Role[] SeedRoles();
        User[] SeedUsers();
        Permission[] SeedPermissions();
    }
}