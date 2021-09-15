using BackendCore.Common.DTO.Permission;


// ReSharper disable once CheckNamespace
namespace BackendCore.Service.Mapping
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