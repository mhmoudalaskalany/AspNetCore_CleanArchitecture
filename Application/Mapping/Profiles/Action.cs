using Common.DTO.Lookup.Action;
using Domain.Entities.Lookup;


// ReSharper disable once CheckNamespace
namespace Application.Mapping
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