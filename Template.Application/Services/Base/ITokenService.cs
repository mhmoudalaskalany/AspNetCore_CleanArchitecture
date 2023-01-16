using Template.Common.DTO.Identity.Account;
using Template.Common.DTO.Identity.User;
using Template.Domain.Entities.Identity;

namespace Template.Application.Services.Base
{
    public interface ITokenService
    {
        UserLoginReturn GenerateJsonWebToken(UserDto userInfo, Role role);
    }
}