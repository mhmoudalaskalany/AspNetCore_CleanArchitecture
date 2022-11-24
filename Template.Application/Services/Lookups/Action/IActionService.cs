using System.Threading.Tasks;
using Application.Services.Base;
using Common.Core;
using Common.DTO.Lookup.Action;

namespace Application.Services.Lookups.Action
{
    public interface IActionService : IBaseService<Domain.Entities.Lookup.Action, AddActionDto , ActionDto , int , int?>
    {
        Task<IFinalResult> GetActionsAsync();
    }
}
