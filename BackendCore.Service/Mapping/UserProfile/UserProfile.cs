using BackendCore.Common.DTO.User;
using BackendCore.Entities.Entities;

namespace BackendCore.Service.Mapping
{
    public partial class MappingService
    {
        public void MapUserProfile()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();

            CreateMap<User, AddUserDto>()
                .ReverseMap();
        }
    }
}