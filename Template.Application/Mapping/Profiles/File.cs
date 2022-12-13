using Common.DTO.Common.File;
using Domain.Entities.Business;

// ReSharper disable once CheckNamespace
namespace Template.Application.Mapping
{
    public partial class MappingService
    {
        public void MapFile()
        {
            CreateMap<File, FileDto>()
                .ReverseMap();

            CreateMap<File, AddFileDto>()
                .ReverseMap();

            CreateMap<File, DownLoadDto>().ReverseMap();
        }
    }
}