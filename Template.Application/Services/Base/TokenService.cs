using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Template.Common.DTO.Identity.Account;
using Template.Common.DTO.Identity.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Template.Domain.Entities.Identity;

namespace Template.Application.Services.Base
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly LoginResponse _userLoginReturn;
        public TokenService(IConfiguration config)
        {
            _config = config;
            _userLoginReturn = new LoginResponse();
        }

        public LoginResponse GenerateJsonWebToken(UserDto userInfo, Role role)
        {
            var claims = new[] {
                new Claim( JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("UserId", userInfo.Id.ToString()),
                new Claim("RoleEn", role.NameEn),
                new Claim("RoleAr", role.NameAr)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var expiryInHours = DateTime.UtcNow.AddHours(Convert.ToDouble(_config["Jwt:ExpiryInHours"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: expiryInHours,
                signingCredentials: credentials,
                claims: claims);

            _userLoginReturn.UserId = userInfo.Id;
            _userLoginReturn.TokenValidTo = token.ValidTo;

            _userLoginReturn.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return _userLoginReturn;
        }
    }
}