using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Common.Core;
using Common.DTO.Lookup.Status;
using Template.Application.Services.Base;

namespace Template.Application.Services.Lookups.Status
{
    public class StatusService : BaseService<Domain.Entities.Lookup.Status, AddStatusDto, StatusDto, int, int?>, IStatusService
    {
        
        public StatusService(IServiceBaseParameter<Domain.Entities.Lookup.Status> parameters) : base(parameters)
        {
        }

        public async Task<IFinalResult> GetStatusesAsync()
        {
            var entities = await UnitOfWork.Repository.FindAsync(x => x.IsDeleted == false);
            var data = Mapper.Map<IEnumerable<Domain.Entities.Lookup.Status>, List<StatusDto>>(entities);
            return new ResponseResult(data, HttpStatusCode.OK, null, "Success");
        }
        
    }
}
