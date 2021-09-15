using System;

namespace BackendCore.Integration.FileRepository.Dtos
{
    public class FileTokenDto
    {
        public Guid FileId { get; set; }
        public string Token { get; set; }
    }
}
