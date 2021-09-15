using BackendCore.Common.DTO.Identity.Login;
using BackendCore.Common.DTO.Identity.User;

namespace BackendCore.Service.Services.Base
{
    public interface ITokenService
    {
        UserLoginReturn GenerateJsonWebToken(UserDto userInfo, string role);
    }
}