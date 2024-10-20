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
        Task<DataPaging> GetAllPagedAsync(BaseParam<ActionFilter> filter);

        Task<DataPaging> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter);
    }
}
