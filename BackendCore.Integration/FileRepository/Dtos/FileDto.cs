﻿using System;
using BackendCore.Common.Core;

namespace BackendCore.Integration.FileRepository.Dtos
{
    public class FileDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public string Url { get; set; }
        public string FileSize { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public string DocumentType { get; set; }
        public string ContentType { get; set; }
    }
}
