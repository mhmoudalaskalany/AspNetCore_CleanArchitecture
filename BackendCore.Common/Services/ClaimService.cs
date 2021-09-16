﻿using BackendCore.Common.DTO.Base;
using Microsoft.AspNetCore.Http;

namespace BackendCore.Common.Services
{
    public class ClaimService : IClaimService
    {
        private readonly HttpContext _context;
        protected TokenClaimDto ClaimData { get; set; }
        public ClaimService()
        {
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            _context = httpContextAccessor.HttpContext;
            SetClaims(_context);

        }

        public void SetClaims(HttpContext context)
        {

            var claims = context?.User;
            ClaimData = new TokenClaimDto()
            {
                UserId = claims?.FindFirst(t => t.Type == "UserId")?.Value,
                Email = claims?.FindFirst(t => t.Type == "Email")?.Value
            };
        }
        public long UserId => ClaimData.UserId != null ? long.Parse(ClaimData.UserId) : 0;

        public string Token => _context.Request.Headers["Authorization"];

    }
}
