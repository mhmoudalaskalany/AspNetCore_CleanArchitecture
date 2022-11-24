﻿using System.Threading.Tasks;
using Application.Services.Base;
using Application.Services.Identity.Account;
using Common.Core;
using Common.DTO.Identity.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Api.Controllers.Base;

namespace Template.Api.Controllers.Identity
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