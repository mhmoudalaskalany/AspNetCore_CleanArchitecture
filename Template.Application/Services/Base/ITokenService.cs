using Common.DTO.Identity.Account;
using Common.DTO.Identity.User;
using Domain.Entities.Identity;

namespace Template.Application.Services.Base
{
    public interface ITokenService
    {
        UserLoginReturn GenerateJsonWebToken(UserDto userInfo, Role role);
    }
}