using Common.DTO.Identity.Account;
using Common.DTO.Identity.User;

namespace Common.Infrastructure.Repository.ActiveDirectory
{
    public interface IActiveDirectoryRepository
    {
        ActiveDirectoryUserDto LoginAsync(LoginParameters parameters);
    }
}
