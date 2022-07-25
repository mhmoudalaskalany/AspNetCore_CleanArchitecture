using System;
using System.Diagnostics.CodeAnalysis;

namespace BackendCore.Common.DTO.Common.File
{
    [ExcludeFromCodeCoverage]
    public class FileTokenDto
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string Extension { get; set; }
    }
}
