using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Base;
using BackendCore.Common.DTO.User;
using BackendCore.Common.DTO.User.Parameters;
using BackendCore.Service.Services.Base;
using LinqKit;

namespace BackendCore.Service.Services.User
{
    public class UserService : BaseService<Entities.Entities.User, AddUserDto, UserDto>, IUserService
    {
        public UserService(IServiceBaseParameter<Entities.Entities.User> parameters) : base(parameters)
        {

        }

        public async Task<DataPaging> GetAllPagedAsync(BaseParam<UserFilter> filter)
        {
            try
            {
                int limit = filter.PageSize;
                int offset = ((--filter.PageNumber) * filter.PageSize);
                var query = await UnitOfWork.Repository.FindPaggedAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
                var data = Mapper.Map<IEnumerable<Entities.Entities.User>, IEnumerable<UserDto>>(query.Item2);
                return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));
            }
            catch (Exception e)
            {
                Result.Message = e.InnerException != null ? e.InnerException.Message : e.Message;
                Result = new ResponseResult(null, status: HttpStatusCode.InternalServerError, exception: e, message: Result.Message);
                return new DataPaging(0, 0, 0, Result);
            }
        }
        static Expression<Func<Entities.Entities.User, bool>> PredicateBuilderFunction(UserFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.User>(true);

            if (!string.IsNullOrWhiteSpace(filter.NameAr))
            {
                predicate = predicate.And(b => b.NameAr.ToLower().Contains(filter.NameAr.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.NameEn))
            {
                predicate = predicate.And(b => b.NameEn.ToLower().Contains(filter.NameEn.ToLower()));
            }
            return predicate;
        }
    }
}
