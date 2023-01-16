using Template.Common.DTO.Lookup.Action;
using Template.Domain.Entities.Lookup;


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