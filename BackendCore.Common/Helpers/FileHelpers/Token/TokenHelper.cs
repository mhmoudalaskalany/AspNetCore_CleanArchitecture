﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BackendCore.Common.DTO.Common.File;
using Microsoft.IdentityModel.Tokens;

namespace BackendCore.Common.Helpers.FileHelpers.Token
{
    public static class TokenHelper
    {
        public static bool CheckToken(string token, Guid id)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token)) return false;
            var issuer = new JwtSecurityTokenHandler().ReadToken(token).Issuer;
            var validTime = new JwtSecurityTokenHandler().ReadToken(token).ValidTo;
            if (validTime > DateTime.UtcNow && issuer.Contains(id.ToString()))
            {
                return true;

            }
            return false;
        }

        public static List<Claim> DecodeToken(string token)
        {
            var claims = (new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken)?.Claims;
            return claims?.ToList();
        }

        /// <summary>
        /// Method to generate token 
        /// </summary>
        /// <param name="expireDate"></param>
        /// <param name="securityKey"></param>
        /// <param name="ids">to make specific</param>
        /// <returns></returns>
        public static List<FileTokenDto> GenerateJsonWebToken(double expireDate, string securityKey, params Guid[] ids)
        {
            var list = new List<FileTokenDto>();
            foreach (var id in ids)
            {
                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey + id));


                var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    id.ToString(),
                        expires: DateTime.UtcNow.AddMinutes(expireDate),
                        signingCredentials: credentials
                 );
                var tokenDto = new FileTokenDto
                {
                    Id = id,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
                list.Add(tokenDto);
            }
            return list;
        }

        /// <summary>
        /// Method to generate token 
        /// </summary>
        /// <param name="expireDate"></param>
        /// <param name="securityKey"></param>
        /// <param name="ids">to make specific</param>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public static List<FileTokenDto> GenerateJsonWebTokenWithClaims(double expireDate, string securityKey, List<Guid> ids , string appCode)
        {
            var list = new List<FileTokenDto>();
            foreach (var id in ids)
            {
                var claims = new[] {
                    new Claim("AppCode",appCode)
                };
                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey + id));

                var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    id.ToString(),
                    expires: DateTime.UtcNow.AddMinutes(expireDate),
                    signingCredentials: credentials,
                    claims:claims
                );
                var tokenDto = new FileTokenDto
                {
                    Id = id,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
                list.Add(tokenDto);
            }
            return list;
        }
    }
}
