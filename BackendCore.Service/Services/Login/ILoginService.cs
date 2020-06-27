using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Login;

namespace BackendCore.Service.Services.Login
{
    public interface ILoginService
    {
        Task<IResult> Login(LoginParameters parameters);
    }
}