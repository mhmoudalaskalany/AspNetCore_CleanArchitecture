using System.Threading.Tasks;
using Template.Common.Core;
using Template.Common.DTO.Identity.Account;

namespace Template.Application.Services.Identity.Account
{
    public interface IAccountService
    {
        Task<IFinalResult> Login(LoginParameters parameters);
        Task<IFinalResult> AdLogin(LoginParameters parameters);
    }
}