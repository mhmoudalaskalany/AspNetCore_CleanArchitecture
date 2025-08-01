﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Common.Core;
using Template.Common.DTO.Lookup.Action;
using Template.Application.Services.Base;
using LinqKit;
using System.Linq.Expressions;
using System;
using Template.Common.DTO.Base;
using Template.Common.DTO.Lookup.Action.Parameters;
using Template.Domain;


namespace Template.Application.Services.Lookups.Action
{
    public class ActionService : BaseService<Domain.Entities.Lookup.Action, AddActionDto, EditActionDto , ActionDto, int, int?>, IActionService
    {
        
        public ActionService(IServiceBaseParameter<Domain.Entities.Lookup.Action> parameters) : base(parameters)
        {
        }


        public async Task<PagedResult<IEnumerable<ActionDto>>> GetAllPagedAsync(BaseParam<ActionFilter> filter)
        {
            var limit = filter.PageSize;

            var offset = ((--filter.PageNumber) * filter.PageSize);

            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: PredicateBuilderFunction(filter.Filter), pageNumber: offset, pageSize: limit, filter.OrderByValue);

            var data = Mapper.Map<IEnumerable<Domain.Entities.Lookup.Action>, IEnumerable<ActionDto>>(query.Item2);

            return PagedResult<IEnumerable<ActionDto>>.Success(data, filter.PageNumber, filter.PageSize, query.Item1, MessagesConstants.Success);
        }


        public async Task<PagedResult<IEnumerable<ActionDto>>> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter)
        {

            var limit = filter.PageSize;

            var offset = ((--filter.PageNumber) * filter.PageSize);

            var predicate = DropDownPredicateBuilderFunction(filter.Filter);

            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: predicate, pageNumber: offset, pageSize: limit);

            var data = Mapper.Map<IEnumerable<Domain.Entities.Lookup.Action>, IEnumerable<ActionDto>>(query.Item2);

            return PagedResult<IEnumerable<ActionDto>>.Success(data, filter.PageNumber, filter.PageSize, query.Item1, MessagesConstants.Success);

        }

        public async Task<Result> DeleteRangeAsync(List<int> ids)
        {
            var rows = await UnitOfWork.Repository.RemoveBulkAsync(x => ids.Contains(x.Id));

            if (rows > 0)
            {
                return Result.Success(MessagesConstants.DeleteSuccess);
            }
            return Result.Failure(MessagesConstants.DeleteError);
        }

        static Expression<Func<Domain.Entities.Lookup.Action, bool>> PredicateBuilderFunction(ActionFilter filter)
        {
            var predicate = PredicateBuilder.New<Domain.Entities.Lookup.Action>(x => x.IsDeleted == filter.IsDeleted);

            if (!string.IsNullOrWhiteSpace(filter?.NameAr))
            {
                predicate = predicate.And(b => b.NameAr.Contains(filter.NameAr));
            }
            if (!string.IsNullOrWhiteSpace(filter?.NameEn))
            {
                predicate = predicate.And(b => b.NameEn.Contains(filter.NameEn));
            }
            return predicate;
        }

        static Expression<Func<Domain.Entities.Lookup.Action, bool>> DropDownPredicateBuilderFunction(SearchCriteriaFilter filter)
        {
            var predicate = PredicateBuilder.New<Domain.Entities.Lookup.Action>(true);
            if (!string.IsNullOrWhiteSpace(filter?.SearchCriteria))
            {
                predicate = predicate.And(b => b.NameAr.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.NameEn.Contains(filter.SearchCriteria));
            }
            return predicate;
        }

    }
}
