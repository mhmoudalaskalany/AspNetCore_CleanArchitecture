using BackendCore.Common.DTO.Identity.Login;
using BackendCore.Common.DTO.Identity.User;

namespace BackendCore.Common.Infrastructure.Repository.ActiveDirectory
{
    public interface IActiveDirectoryRepository
    {
        ActiveDirectoryUserDto LoginAsync(LoginParameters parameters);
    }
}
