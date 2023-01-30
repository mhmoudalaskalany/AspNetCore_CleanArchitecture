using System.Threading.Tasks;
using Template.Application.Services.Base;
using Template.Common.Core;
using Template.Common.DTO.Lookup.Category;

namespace Template.Application.Services.Lookups.Category
{
    public interface ICategoryService : IBaseService<Domain.Entities.Lookup.Category, AddCategoryDto , EditCategoryDto , CategoryDto , int , int?>
    {
        Task<IFinalResult> GetCategoriesAsync();
    }
}
