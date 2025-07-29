using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.DTO.Identity.User;
using Template.Common.DTO.Identity.User.Parameters;
using LinqKit;
using Template.Application.Services.Base;
using Template.Domain;

namespace Template.Application.Services.Identity.User
{
    public class UserService : BaseService<Domain.Entities.Identity.User, AddUserDto , EditUserDto, UserDto, Guid, Guid?>, IUserService
    {
        public UserService(IServiceBaseParameter<Domain.Entities.Identity.User> parameters) : base(parameters)
        {

        }

        public async Task<PagedResult<IEnumerable<UserDto>>> GetAllPagedAsync(BaseParam<UserFilter> filter)
        {

            var limit = filter.PageSize;

            var offset = ((--filter.PageNumber) * filter.PageSize);

            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: PredicateBuilderFunction(filter.Filter), pageNumber: offset, pageSize: limit, filter.OrderByValue);

            var data = Mapper.Map<IEnumerable<Domain.Entities.Identity.User>, IEnumerable<UserDto>>(query.Item2);

            return PagedResult<IEnumerable<UserDto>>.Success(data, filter.PageNumber, filter.PageSize, query.Item1, MessagesConstants.Success);

        }


        public async Task<PagedResult<IEnumerable<UserDto>>> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter)
        {

            var limit = filter.PageSize;

            var offset = ((--filter.PageNumber) * filter.PageSize);

            var predicate = DropDownPredicateBuilderFunction(filter.Filter);

            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: predicate, pageNumber: offset, pageSize: limit);

            var data = Mapper.Map<IEnumerable<Domain.Entities.Identity.User>, IEnumerable<UserDto>>(query.Item2);

            return PagedResult<IEnumerable<UserDto>>.Success(data, filter.PageNumber, filter.PageSize, query.Item1, MessagesConstants.Success);

        }

        public async Task<Result> DeleteRangeAsync(List<Guid> ids)
        {
            var rows = await UnitOfWork.Repository.RemoveBulkAsync(x => ids.Contains(x.Id));

            if (rows > 0)
            {
                return Result.Success(MessagesConstants.DeleteSuccess);
            }
            return Result.Failure(MessagesConstants.DeleteError);
        }

        static Expression<Func<Domain.Entities.Identity.User, bool>> PredicateBuilderFunction(UserFilter filter)
        {
            var predicate = PredicateBuilder.New<Domain.Entities.Identity.User>(x => x.IsDeleted == filter.IsDeleted);

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

        static Expression<Func<Domain.Entities.Identity.User, bool>> DropDownPredicateBuilderFunction(SearchCriteriaFilter filter)
        {
            var predicate = PredicateBuilder.New<Domain.Entities.Identity.User>(true);
            if (!string.IsNullOrWhiteSpace(filter?.SearchCriteria))
            {
                predicate = predicate.And(b => b.NameAr.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.NameEn.Contains(filter.SearchCriteria));
            }
            return predicate;
        }


    }
}
