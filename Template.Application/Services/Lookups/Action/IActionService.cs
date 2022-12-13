using System.Threading.Tasks;
using Common.Core;
using Common.DTO.Lookup.Action;
using Template.Application.Services.Base;

namespace Template.Application.Services.Lookups.Action
{
    public interface IActionService : IBaseService<Domain.Entities.Lookup.Action, AddActionDto , ActionDto , int , int?>
    {
        Task<IFinalResult> GetActionsAsync();
    }
}
