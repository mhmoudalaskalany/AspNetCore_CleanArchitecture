using BackendCore.Common.DTO.Permission;

namespace BackendCore.Service.Mapping.Permission
{
    public partial class MappingService
    {
        public void MapPermission()
        {
            CreateMap<Entities.Entities.Permission, PermissionDto>()
                .ReverseMap();

            CreateMap<Entities.Entities.Permission, AddPermissionDto>()
                .ReverseMap();
        }
    }
}