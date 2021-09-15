using BackendCore.Common.DTO.Lookup.Status;
using BackendCore.Entities.Entities.Lookup;


// ReSharper disable once CheckNamespace
namespace BackendCore.Service.Mapping
{
    public partial class MappingService
    {
        public void MapStatus()
        {
            CreateMap<Status, AddStatusDto>()
                .ReverseMap();

            CreateMap<Status, StatusDto>()
                .ReverseMap();
        }
    }
}