using System;

namespace BackendCore.Common.DTO.Common.File
{
    public class UploadResponseDto
    {
        public Guid FileId { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentExtension { get; set; }
        public string AttachmentSize { get; set; }
        public string AttachmentType { get; set; }
    }
}
