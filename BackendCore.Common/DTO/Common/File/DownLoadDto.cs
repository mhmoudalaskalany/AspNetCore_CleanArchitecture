using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace BackendCore.Common.DTO.Common.File
{
    [ExcludeFromCodeCoverage]
    public class DownLoadDto
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public bool IsPublic { get; set; }
        public MemoryStream MemoryStream { get; set; }
    }
}
