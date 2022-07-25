using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Lookup.Status;
using BackendCore.Service.Services.Base;

namespace BackendCore.Service.Services.Lookups.Status
{
    public class StatusService : BaseService<Entities.Entities.Lookup.Status, AddStatusDto, StatusDto, int, int?>, IStatusService
    {
        
        public StatusService(IServiceBaseParameter<Entities.Entities.Lookup.Status> parameters) : base(parameters)
        {
        }


        public async Task<IFinalResult> GetStatusesAsync()
        {
            var entities = await UnitOfWork.Repository.FindAsync(x => x.IsDeleted == false);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Lookup.Status>, List<StatusDto>>(entities);
            return new ResponseResult(data, HttpStatusCode.OK, null, "Success");
        }
        
    }
}
