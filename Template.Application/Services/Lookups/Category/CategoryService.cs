using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Template.Application.Services.Base;
using Template.Common.Core;
using Template.Common.DTO.Lookup.Category;

namespace Template.Application.Services.Lookups.Category
{
    public class CategoryService : BaseService<Domain.Entities.Lookup.Category, AddCategoryDto, EditCategoryDto , CategoryDto, int, int?>, ICategoryService
    {
        
        public CategoryService(IServiceBaseParameter<Domain.Entities.Lookup.Category> parameters) : base(parameters)
        {
        }


        public async Task<IFinalResult> GetCategoriesAsync()
        {
            var entities = await UnitOfWork.GetRepository<Domain.Entities.Lookup.Category>().FindAsync(x => x.IsDeleted == false);
            var data = Mapper.Map<IEnumerable<Domain.Entities.Lookup.Category>, List<CategoryDto>>(entities);
            return new ResponseResult(data, HttpStatusCode.OK, null, "Success");
        }

    }
}
