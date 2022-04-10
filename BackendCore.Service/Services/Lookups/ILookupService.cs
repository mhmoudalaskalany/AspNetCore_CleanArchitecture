using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Lookup.Status;
using BackendCore.Entities.Entities.Lookup;
using BackendCore.Service.Services.Base;

namespace BackendCore.Service.Services.Lookups
{
    public interface ILookupService : IBaseService<Status , AddStatusDto , StatusDto , long , long?>
    {
        Task<IFinalResult> GetStatusesAsync();
        Task<IFinalResult> GetActionsAsync();
    }
}
