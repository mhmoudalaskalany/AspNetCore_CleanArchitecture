using System;
using System.Diagnostics.CodeAnalysis;
using BackendCore.Common.DTO.Base;

namespace BackendCore.Common.DTO.Common.File.Parameters
{
    [ExcludeFromCodeCoverage]
    public class FileFilter : MainFilter
    {
        public Guid? Id { get; set; }
        public string AppCode { get; set; }
    }
}
