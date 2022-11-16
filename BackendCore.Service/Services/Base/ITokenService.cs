using BackendCore.Common.DTO.Identity.Login;
using BackendCore.Common.DTO.Identity.User;
using BackendCore.Entities.Entities.Identity;

namespace BackendCore.Service.Services.Base
{
    public interface ITokenService
    {
        UserLoginReturn GenerateJsonWebToken(UserDto userInfo, Role role);
    }
}