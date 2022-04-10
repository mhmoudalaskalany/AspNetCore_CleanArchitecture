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
    /// Accounts Controller
    /// </summary>
    public class AccountsController : BaseController
    {
        private readonly IAccountService _accountService;
        /// <inheritdoc />
        public AccountsController(IAccountService accountService, ITokenService tokenService) : base(tokenService)
        {
            _accountService = accountService;
        }
        /// <summary>
        /// Login 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IFinalResult> Login(LoginParameters parameter)
        {
            return await _accountService.Login(parameter);
        }
    }
}
