using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.V1.Base;
using Template.Application.Services.Base;
using Template.Application.Services.Identity.Account;
using Template.Common.Core;
using Template.Common.DTO.Identity.Account;
using Template.Common.Extensions;

namespace Template.Api.Controllers.V1.Identity
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
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), 200)]
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), 400)]
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), 401)]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> Login(LoginParameters parameter)
        {
            var result = await _service.Login(parameter);
            return result.ToActionResult();
        }
    }
}
