using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.Base.V1;
using Template.Application.Services.Base;
using Template.Application.Services.Identity.Account;
using Template.Common.Core;
using Template.Common.DTO.Identity.Account;

namespace Template.Api.Controllers.Identity.V1
{
    /// <summary>
    /// Accounts Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountsController : BaseController
    {
        private readonly IAccountService _service;
        /// <inheritdoc />
        public AccountsController(IAccountService accountService, ITokenService tokenService) : base(tokenService)
        {
            _service = accountService;
        }
        /// <summary>
        /// Login 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IFinalResult> Login(LoginParameters parameter)
        {
            return await _service.Login(parameter);
        }
    }
}
