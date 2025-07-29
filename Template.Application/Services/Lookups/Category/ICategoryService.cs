using System.Collections.Generic;
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
        Task<PagedResult<IEnumerable<CategoryDto>>> GetAllPagedAsync(BaseParam<CategoryFilter> filter);

        Task<PagedResult<IEnumerable<CategoryDto>>> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter);

        Task<Result> DeleteRangeAsync(List<int> ids);
    }
}
