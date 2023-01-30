using Template.Common.DTO.Lookup.Category;
using Template.Domain.Entities.Lookup;


// ReSharper disable once CheckNamespace
namespace Template.Application.Mapping
{
    public partial class MappingService
    {
        public void MapCategory()
        {
            CreateMap<Category, CategoryDto>()
                .ReverseMap();

            CreateMap<Category, AddCategoryDto>()
                .ReverseMap();

            CreateMap<Category, EditCategoryDto>()
                .ReverseMap();
        }
    }
}