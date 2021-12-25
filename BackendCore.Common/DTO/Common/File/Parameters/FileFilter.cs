using System;
using BackendCore.Common.DTO.Base;

namespace BackendCore.Common.DTO.Common.File.Parameters
{
    public class FileFilter : MainFilter
    {
        public Guid? Id { get; set; }
        public string AppCode { get; set; }
    }
}
