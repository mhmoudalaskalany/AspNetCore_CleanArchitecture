using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Lookup.Action;
using BackendCore.Common.DTO.Lookup.Status;
using BackendCore.Entities.Entities.Lookup;
using BackendCore.Service.Services.Base;

namespace BackendCore.Service.Services.Lookups
{
    public class LookupService : BaseService<Status, AddStatusDto, StatusDto, int, int?>, ILookupService
    {
        
        public LookupService(IServiceBaseParameter<Status> parameters) : base(parameters)
        {
        }


        public async Task<IFinalResult> GetStatusesAsync()
        {
            var entities = await UnitOfWork.Repository.FindAsync(x => x.IsDeleted == false);
            var data = Mapper.Map<IEnumerable<Status>, List<StatusDto>>(entities);
            return new ResponseResult(data, HttpStatusCode.OK, null, "Success");
        }
        
        public async Task<IFinalResult> GetActionsAsync()
        {
            var entities = await UnitOfWork.GetRepository<Action>().FindAsync(x => x.IsDeleted == false);
            var data = Mapper.Map<IEnumerable<Action>, List<ActionDto>>(entities);
            return new ResponseResult(data, HttpStatusCode.OK, null, "Success");
        }





    }
}
