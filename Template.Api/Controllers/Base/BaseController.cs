﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Services.Base;

namespace Template.Api.Controllers.Base
{
    /// <inheritdoc />
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Token Business Property
        /// </summary>
        protected readonly ITokenService TokenService;
        /// <inheritdoc />
        public BaseController()
        {

        }
        /// <inheritdoc />
        public BaseController(ITokenService tokenService)
        {
            TokenService = tokenService;
        }
    }
}