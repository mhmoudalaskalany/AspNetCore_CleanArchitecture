﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Template.Common.DTO.Common.File
{
    [ExcludeFromCodeCoverage]
    public class TokenDto
    {
        public Guid Id { get; set; }

        public string Token { get; set; }
    }
}
