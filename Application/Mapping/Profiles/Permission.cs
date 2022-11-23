using Common.DTO.Identity.Permission;
using Domain.Entities.Identity;


// ReSharper disable once CheckNamespace
namespace Application.Mapping
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