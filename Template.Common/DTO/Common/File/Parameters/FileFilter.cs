using System;
using System.Diagnostics.CodeAnalysis;
using Template.Common.DTO.Base;

namespace Template.Common.DTO.Common.File.Parameters
{
    [ExcludeFromCodeCoverage]
    public class FileFilter : MainFilter
    {
        public Guid? Id { get; set; }

        public string AppCode { get; set; }
    }
}
