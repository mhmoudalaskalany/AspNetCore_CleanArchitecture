using System.Threading.Tasks;
using Common.Core;
using Common.DTO.Lookup.Status;
using Template.Application.Services.Base;

namespace Template.Application.Services.Lookups.Status
{
    public interface IStatusService : IBaseService<Domain.Entities.Lookup.Status , AddStatusDto , StatusDto , int , int?>
    {
        Task<IFinalResult> GetStatusesAsync();
    }
}
