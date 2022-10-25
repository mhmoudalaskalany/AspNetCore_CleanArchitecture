using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Lookup.Action;
using BackendCore.Service.Services.Base;

namespace BackendCore.Service.Services.Lookups.Action
{
    public class ActionService : BaseService<Entities.Entities.Lookup.Action, AddActionDto, ActionDto, int, int?>, IActionService
    {
        
        public ActionService(IServiceBaseParameter<Entities.Entities.Lookup.Action> parameters) : base(parameters)
        {
        }


        public async Task<IFinalResult> GetActionsAsync()
        {
            var entities = await UnitOfWork.GetRepository<Entities.Entities.Lookup.Action>().FindAsync(x => x.IsDeleted == false);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Lookup.Action>, List<ActionDto>>(entities);
            return new ResponseResult(data, HttpStatusCode.OK, null, "Success");
        }

    }
}
