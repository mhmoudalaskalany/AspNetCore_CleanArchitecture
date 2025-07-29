using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Common.Core;
using Template.Common.DTO.Lookup.Action;
using Template.Application.Services.Base;
using Template.Common.DTO.Base;
using Template.Common.DTO.Lookup.Action.Parameters;

namespace Template.Application.Services.Lookups.Action
{
    public interface IActionService : IBaseService<Domain.Entities.Lookup.Action, AddActionDto , EditActionDto , ActionDto , int , int?>
    {
        Task<PagedResult<IEnumerable<ActionDto>>> GetAllPagedAsync(BaseParam<ActionFilter> filter);

        Task<PagedResult<IEnumerable<ActionDto>>> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter);

        Task<Result> DeleteRangeAsync(List<int> ids);
    }
}
