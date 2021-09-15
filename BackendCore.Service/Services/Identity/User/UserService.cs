using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Base;
using BackendCore.Common.DTO.Identity.User;
using BackendCore.Common.DTO.Identity.User.Parameters;
using BackendCore.Service.Services.Base;
using LinqKit;

namespace BackendCore.Service.Services.Identity.User
{
    public class UserService : BaseService<Entities.Entities.Identity.User, AddUserDto, UserDto, Guid, Guid?>, IUserService
    {
        public UserService(IServiceBaseParameter<Entities.Entities.Identity.User> parameters) : base(parameters)
        {

        }

        #region Public Methods
        /// <summary>
        /// Get All Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<DataPaging> GetAllPagedAsync(BaseParam<UserFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.User>, IEnumerable<UserDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, result: data, status: HttpStatusCode.OK, HttpStatusCode.OK.ToString());

        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Predicate Builder
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        static Expression<Func<Entities.Entities.Identity.User, bool>> PredicateBuilderFunction(UserFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Identity.User>(true);

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

        #endregion


    }
}
