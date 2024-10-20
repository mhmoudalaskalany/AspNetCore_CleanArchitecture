using System.Threading.Tasks;
using Template.Application.Services.Base;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.DTO.Lookup.Category;
using Template.Common.DTO.Lookup.Category.Parameters;

namespace Template.Application.Services.Lookups.Category
{
    public interface ICategoryService : IBaseService<Domain.Entities.Lookup.Category, AddCategoryDto , EditCategoryDto , CategoryDto , int , int?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<CategoryFilter> filter);

        Task<DataPaging> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter);
    }
}
