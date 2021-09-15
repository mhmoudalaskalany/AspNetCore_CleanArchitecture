using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Base;
using BackendCore.Common.DTO.Identity.Permission;
using BackendCore.Common.DTO.Identity.Permission.Parameters;
using BackendCore.Service.Services.Base;
using LinqKit;

namespace BackendCore.Service.Services.Identity.Permission
{
    public class PermissionService : BaseService<Entities.Entities.Identity.Permission, AddPermissionDto, PermissionDto, long, long?>, IPermissionService
    {
        #region Properties
        #endregion

        #region Constructors
        public PermissionService(IServiceBaseParameter<Entities.Entities.Identity.Permission> parameters) : base(parameters)
        {

        }
        #endregion


        #region Public Methods
        /// <summary>
        /// Get All Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<DataPaging> GetAllPagedAsync(BaseParam<PermissionFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.Permission>, IEnumerable<PermissionDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, result: data, status: HttpStatusCode.OK, HttpStatusCode.OK.ToString());

        }

        #endregion


        #region Private Methods
        /// <summary>
        /// Predicate Builder For Get Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        static Expression<Func<Entities.Entities.Identity.Permission, bool>> PredicateBuilderFunction(PermissionFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Identity.Permission>(true);

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
