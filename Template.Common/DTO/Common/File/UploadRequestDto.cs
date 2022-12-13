using System.Diagnostics.CodeAnalysis;
using Domain.Enum;

namespace Template.Common.DTO.Common.File
{
    [ExcludeFromCodeCoverage]
    public class UploadRequestDto
    {
        public byte[] FileBytes { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public bool IsPublic { get; set; }
        public string AttachmentExtension { get; set; }
        public string AccessLevelCode { get; set; }
        public string CategoryCode { get; set; }
        public string AppCode { get; set; }
        public StorageType StorageType { get; set; }
    }
}
