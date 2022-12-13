using Common.DTO.Business.Attachment;
using Domain.Entities.Business;

// ReSharper disable once CheckNamespace
namespace Template.Application.Mapping
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