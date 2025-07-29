using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.DTO.Identity.Permission;
using Template.Common.DTO.Identity.Permission.Parameters;
using LinqKit;
using Template.Application.Services.Base;
using Template.Domain;

namespace Template.Application.Services.Identity.Permission
{
    public class PermissionService : BaseService<Domain.Entities.Identity.Permission, AddPermissionDto , EditPermissionDto, PermissionDto, int, int?>, IPermissionService
    {
        public PermissionService(IServiceBaseParameter<Domain.Entities.Identity.Permission> parameters) : base(parameters)
        {

        }

        public async Task<PagedResult<IEnumerable<PermissionDto>>> GetAllPagedAsync(BaseParam<PermissionFilter> filter)
        {

            var limit = filter.PageSize;

            var offset = ((--filter.PageNumber) * filter.PageSize);

            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: PredicateBuilderFunction(filter.Filter), pageNumber: offset, pageSize: limit, filter.OrderByValue);

            var data = Mapper.Map<IEnumerable<Domain.Entities.Identity.Permission>, IEnumerable<PermissionDto>>(query.Item2);

            return PagedResult<IEnumerable<PermissionDto>>.Success(data, filter.PageNumber, filter.PageSize, query.Item1, MessagesConstants.Success);

        }


        public async Task<PagedResult<IEnumerable<PermissionDto>>> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter)
        {

            var limit = filter.PageSize;

            var offset = ((--filter.PageNumber) * filter.PageSize);

            var predicate = DropDownPredicateBuilderFunction(filter.Filter);

            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: predicate, pageNumber: offset, pageSize: limit);

            var data = Mapper.Map<IEnumerable<Domain.Entities.Identity.Permission>, IEnumerable<PermissionDto>>(query.Item2);

            return PagedResult<IEnumerable<PermissionDto>>.Success(data, filter.PageNumber, filter.PageSize, query.Item1, MessagesConstants.Success);

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

        static Expression<Func<Domain.Entities.Identity.Permission, bool>> PredicateBuilderFunction(PermissionFilter filter)
        {
            var predicate = PredicateBuilder.New<Domain.Entities.Identity.Permission>(x => x.IsDeleted == filter.IsDeleted);

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

        static Expression<Func<Domain.Entities.Identity.Permission, bool>> DropDownPredicateBuilderFunction(SearchCriteriaFilter filter)
        {
            var predicate = PredicateBuilder.New<Domain.Entities.Identity.Permission>(true);
            if (!string.IsNullOrWhiteSpace(filter?.SearchCriteria))
            {
                predicate = predicate.And(b => b.NameAr.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.NameEn.Contains(filter.SearchCriteria));
            }
            return predicate;
        }

    }
}
