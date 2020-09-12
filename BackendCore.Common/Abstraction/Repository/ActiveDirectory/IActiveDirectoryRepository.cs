using BackendCore.Common.DTO.Login;
using BackendCore.Common.DTO.User;

namespace BackendCore.Common.Abstraction.Repository.ActiveDirectory
{
    public interface IActiveDirectoryRepository
    {
        ActiveDirectoryUserDto LoginAsync(LoginParameters parameters);
    }
}
