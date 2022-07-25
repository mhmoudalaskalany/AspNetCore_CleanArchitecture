using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Lookup.Action;
using BackendCore.Service.Services.Base;

namespace BackendCore.Service.Services.Lookups.Action
{
    public interface IActionService : IBaseService<Entities.Entities.Lookup.Action, AddActionDto , ActionDto , int , int?>
    {
        Task<IFinalResult> GetActionsAsync();
    }
}
