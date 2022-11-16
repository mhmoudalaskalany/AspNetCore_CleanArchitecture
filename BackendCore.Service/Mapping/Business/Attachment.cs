
using BackendCore.Common.DTO.Business.Attachment;
using BackendCore.Entities.Entities.Business;

// ReSharper disable once CheckNamespace
namespace BackendCore.Service.Mapping
{
    public partial class MappingService
    {
        public void MapAttachment()
        {
            CreateMap<Attachment, AttachmentDto>()
                .ReverseMap();

            CreateMap<Attachment, AddAttachmentDto>()
                .ReverseMap();
        }
    }
}