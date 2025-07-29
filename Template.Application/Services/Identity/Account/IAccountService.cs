using System.Threading.Tasks;
using Template.Common.Core;
using Template.Common.DTO.Identity.Account;

namespace Template.Application.Services.Identity.Account
{
    public interface IAccountService
    {
        Task<Result<LoginResponse>> Login(LoginParameters parameters);

        Task<Result<LoginResponse>> AdLogin(LoginParameters parameters);
    }
}