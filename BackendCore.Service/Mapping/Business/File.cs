using BackendCore.Common.DTO.Common.File;
using BackendCore.Entities.Entities.Business;

// ReSharper disable once CheckNamespace
namespace BackendCore.Service.Mapping
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