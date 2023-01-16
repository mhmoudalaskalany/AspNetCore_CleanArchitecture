using Template.Common.DTO.Identity.Account;
using Template.Common.DTO.Identity.User;

namespace Template.Common.Infrastructure.Repository.ActiveDirectory
{
    public interface IActiveDirectoryRepository
    {
        ActiveDirectoryUserDto LoginAsync(LoginParameters parameters);
    }
}
