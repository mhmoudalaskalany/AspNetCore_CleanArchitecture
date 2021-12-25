using BackendCore.Entities.Enum;

namespace BackendCore.Common.DTO.Common.File
{
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
