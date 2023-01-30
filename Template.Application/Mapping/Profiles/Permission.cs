using Template.Common.DTO.Identity.Permission;
using Template.Domain.Entities.Identity;


// ReSharper disable once CheckNamespace
namespace Template.Application.Mapping
{
    public partial class MappingService
    {
        public void MapPermission()
        {
            CreateMap<Permission, PermissionDto>()
                .ReverseMap();

            CreateMap<Permission, AddPermissionDto>()
                .ReverseMap();

            CreateMap<Permission, EditPermissionDto>()
                .ReverseMap();
        }
    }
}