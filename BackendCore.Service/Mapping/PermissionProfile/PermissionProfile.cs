using BackendCore.Common.DTO.Permission;
using BackendCore.Entities.Entities;

namespace BackendCore.Service.Mapping
{
    public partial class MappingService
    {
        public void MapPermissionProfile()
        {
            CreateMap<Permission, PermissionDto>()
                .ReverseMap();

            CreateMap<Permission, AddPermissionDto>()
                .ReverseMap();
        }
    }
}