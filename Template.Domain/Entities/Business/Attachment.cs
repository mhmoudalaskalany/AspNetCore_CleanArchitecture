using System;
using System.Diagnostics.CodeAnalysis;
using Template.Domain.Entities.Base;

namespace Template.Domain.Entities.Business
{
    [ExcludeFromCodeCoverage]
    public class Attachment : BaseEntity<Guid>
    {
        public Guid FileId { get; set; }

        public string FileName { get; set; }

        public string Extension { get; set; }

        public string Size { get; set; }

        public bool IsPublic { get; set; }

        public string AttachmentDisplaySize { get; set; }
    }
}
