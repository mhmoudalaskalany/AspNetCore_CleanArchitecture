using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Template.Common.Core;
using Template.Common.DTO.Lookup.Action;
using Template.Application.Services.Base;


namespace Template.Application.Services.Lookups.Action
{
    public class ActionService : BaseService<Domain.Entities.Lookup.Action, AddActionDto, EditActionDto , ActionDto, int, int?>, IActionService
    {
        
        public ActionService(IServiceBaseParameter<Domain.Entities.Lookup.Action> parameters) : base(parameters)
        {
        }


        public async Task<IFinalResult> GetActionsAsync()
        {
            var entities = await UnitOfWork.GetRepository<Domain.Entities.Lookup.Action>().FindAsync(x => x.IsDeleted == false);
            var data = Mapper.Map<IEnumerable<Domain.Entities.Lookup.Action>, List<ActionDto>>(entities);
            return new ResponseResult(data, HttpStatusCode.OK, null, "Success");
        }

    }
}
