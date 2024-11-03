﻿using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Template.Common.Core;
using Template.Common.DTO.Lookup.Status;
using Template.Application.Services.Base;
using LinqKit;
using System.Linq.Expressions;
using System;
using Template.Common.DTO.Base;
using Template.Common.DTO.Lookup.Status.Parameters;
using Template.Domain;

namespace Template.Application.Services.Lookups.Status
{
    public class StatusService : BaseService<Domain.Entities.Lookup.Status, AddStatusDto, EditStatusDto, StatusDto, int, int?>, IStatusService
    {
        
        public StatusService(IServiceBaseParameter<Domain.Entities.Lookup.Status> parameters) : base(parameters)
        {
        }

        public async Task<DataPaging> GetAllPagedAsync(BaseParam<StatusFilter> filter)
        {

            var limit = filter.PageSize;

            var offset = ((--filter.PageNumber) * filter.PageSize);

            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: PredicateBuilderFunction(filter.Filter), pageNumber: offset, pageSize: limit, filter.OrderByValue);

            var data = Mapper.Map<IEnumerable<Domain.Entities.Lookup.Status>, IEnumerable<StatusDto>>(query.Item2);

            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, result: data, status: HttpStatusCode.OK, HttpStatusCode.OK.ToString());

        }


        public async Task<DataPaging> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter)
        {

            var limit = filter.PageSize;

            var offset = ((--filter.PageNumber) * filter.PageSize);

            var predicate = DropDownPredicateBuilderFunction(filter.Filter);

            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: predicate, pageNumber: offset, pageSize: limit);

            var data = Mapper.Map<IEnumerable<Domain.Entities.Lookup.Status>, IEnumerable<StatusDto>>(query.Item2);

            return new DataPaging(filter.PageNumber, filter.PageSize, query.Item1, data, status: HttpStatusCode.OK, MessagesConstants.Success);

        }

        public async Task<IFinalResult> DeleteRangeAsync(List<int> ids)
        {
            var rows = await UnitOfWork.Repository.RemoveBulkAsync(x => ids.Contains(x.Id));
            if (rows > 0)
            {
                return new ResponseResult().PostResult(result: true, status: HttpStatusCode.OK, message: MessagesConstants.DeleteSuccess);
            }
            return new ResponseResult().PostResult(result: false, status: HttpStatusCode.BadRequest, message: MessagesConstants.DeleteError);
        }

        static Expression<Func<Domain.Entities.Lookup.Status, bool>> PredicateBuilderFunction(StatusFilter filter)
        {
            var predicate = PredicateBuilder.New<Domain.Entities.Lookup.Status>(x => x.IsDeleted == filter.IsDeleted);

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

        static Expression<Func<Domain.Entities.Lookup.Status, bool>> DropDownPredicateBuilderFunction(SearchCriteriaFilter filter)
        {
            var predicate = PredicateBuilder.New<Domain.Entities.Lookup.Status>(true);
            if (!string.IsNullOrWhiteSpace(filter?.SearchCriteria))
            {
                predicate = predicate.And(b => b.NameAr.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.NameEn.Contains(filter.SearchCriteria));
            }
            return predicate;
        }

    }
}
