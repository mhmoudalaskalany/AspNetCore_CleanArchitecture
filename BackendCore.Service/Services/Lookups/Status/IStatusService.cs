using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Lookup.Status;
using BackendCore.Service.Services.Base;

namespace BackendCore.Service.Services.Lookups.Status
{
    public interface IStatusService : IBaseService<Entities.Entities.Lookup.Status , AddStatusDto , StatusDto , int , int?>
    {
        Task<IFinalResult> GetStatusesAsync();
    }
}
