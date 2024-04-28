using System.Threading.Tasks;
using Template.Common.Core;
using Template.Common.DTO.Lookup.Status;
using Template.Application.Services.Base;
using Template.Common.DTO.Base;
using Template.Common.DTO.Lookup.Status.Parameters;

namespace Template.Application.Services.Lookups.Status
{
    public interface IStatusService : IBaseService<Domain.Entities.Lookup.Status , AddStatusDto ,EditStatusDto, StatusDto , int , int?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<StatusFilter> filter);
    }
}
