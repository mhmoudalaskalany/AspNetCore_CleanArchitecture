using Domain.Entities.Lookup;
using Template.Common.DTO.Lookup.Action;


// ReSharper disable once CheckNamespace
namespace Template.Application.Mapping
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