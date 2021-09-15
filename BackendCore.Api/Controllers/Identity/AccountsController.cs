using System.Threading.Tasks;
using BackendCore.Api.Controllers.Base;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Identity.Login;
using BackendCore.Service.Services.Base;
using BackendCore.Service.Services.Identity.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendCore.Api.Controllers.Identity
{
    /// <summary>
    /// Account Controller
    /// </summary>
    public class AccountsController : BaseController
    {
        private readonly ILoginService _loginServices;
        /// <inheritdoc />
        public AccountsController(ILoginService loginServices, ITokenService tokenService) : base(tokenService)
        {
            _loginServices = loginServices;
        }
        /// <summary>
        /// Login 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IResult> Login(LoginParameters parameter)
        {
            return await _loginServices.Login(parameter);
        }
    }
}
