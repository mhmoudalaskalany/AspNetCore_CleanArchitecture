using System.Threading.Tasks;
using Common.Core;
using Common.DTO.Identity.Account;

namespace Application.Services.Identity.Account
{
    public interface IAccountService
    {
        Task<IFinalResult> Login(LoginParameters parameters);
        Task<IFinalResult> AdLogin(LoginParameters parameters);
    }
}