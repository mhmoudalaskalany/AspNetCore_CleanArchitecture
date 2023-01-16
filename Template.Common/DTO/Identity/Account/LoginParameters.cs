﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Template.Common.DTO.Identity.Account
{
    [ExcludeFromCodeCoverage]
    public class LoginParameters
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}