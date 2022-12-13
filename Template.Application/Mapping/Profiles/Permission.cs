using Domain.Entities.Identity;
using Template.Common.DTO.Identity.Permission;


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
        }
    }
}