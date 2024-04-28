using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Template.Common.Core;
using Template.Common.DTO.Lookup.Action;
using Template.Application.Services.Base;
using LinqKit;
using System.Linq.Expressions;
using System;
using Template.Common.DTO.Base;
using Template.Common.DTO.Lookup.Action.Parameters;


namespace Template.Application.Services.Lookups.Action
{
    public class ActionService : BaseService<Domain.Entities.Lookup.Action, AddActionDto, EditActionDto , ActionDto, int, int?>, IActionService
    {
        
        public ActionService(IServiceBaseParameter<Domain.Entities.Lookup.Action> parameters) : base(parameters)
        {
        }


        public async Task<DataPaging> GetAllPagedAsync(BaseParam<ActionFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<Domain.Entities.Lookup.Action>, IEnumerable<ActionDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, result: data, status: HttpStatusCode.OK, HttpStatusCode.OK.ToString());

        }

        static Expression<Func<Domain.Entities.Lookup.Action, bool>> PredicateBuilderFunction(ActionFilter filter)
        {
            var predicate = PredicateBuilder.New<Domain.Entities.Lookup.Action>(x => x.IsDeleted == filter.IsDeleted);

            if (!string.IsNullOrWhiteSpace(filter?.NameAr))
            {
                predicate = predicate.And(b => b.NameAr.ToLower().Contains(filter.NameAr.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter?.NameEn))
            {
                predicate = predicate.And(b => b.NameEn.ToLower().Contains(filter.NameEn.ToLower()));
            }
            return predicate;
        }

    }
}
