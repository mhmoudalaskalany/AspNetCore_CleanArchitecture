using BackendCore.Common.DTO.Lookup.Action;
using BackendCore.Entities.Entities.Lookup;


// ReSharper disable once CheckNamespace
namespace BackendCore.Service.Mapping
{
    public partial class MappingService
    {
        public void MapAction()
        {
            CreateMap<Action, ActionDto>()
                .ReverseMap();

            CreateMap<Action, AddActionDto>()
                .ReverseMap();
        }
    }
}