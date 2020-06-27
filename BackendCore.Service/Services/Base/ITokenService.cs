using BackendCore.Common.DTO.Login;
using BackendCore.Common.DTO.User;

namespace BackendCore.Service.Services.Base
{
    public interface ITokenService
    {
        UserLoginReturn GenerateJsonWebToken(UserDto userInfo, string role);
    }
}