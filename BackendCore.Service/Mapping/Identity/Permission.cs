using BackendCore.Common.DTO.Identity.Permission;
using BackendCore.Entities.Entities.Identity;


// ReSharper disable once CheckNamespace
namespace BackendCore.Service.Mapping
{
    public partial class MappingService
    {
        public void MapPermission()
        {
            CreateMap<Permission, PermissionDto>()
                .ReverseMap();

            CreateMap<Permission, AddPermissionDto>()
                .ReverseMap();
        }
    }
}