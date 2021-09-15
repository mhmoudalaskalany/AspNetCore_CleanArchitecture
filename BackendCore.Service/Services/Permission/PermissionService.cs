using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Base;
using BackendCore.Common.DTO.Permission;
using BackendCore.Common.DTO.Permission.Parameters;
using BackendCore.Service.Services.Base;
using LinqKit;

namespace BackendCore.Service.Services.Permission
{
    public class PermissionService : BaseService<Entities.Entities.Identity.Permission, AddPermissionDto, PermissionDto , long , long?>, IPermissionService
    {
        public PermissionService(IServiceBaseParameter<Entities.Entities.Identity.Permission> parameters) : base(parameters)
        {

        }

        public async Task<DataPaging> GetAllPagedAsync(BaseParam<PermissionFilter> filter)
        {
            try
            {
                int limit = filter.PageSize;
                int offset = ((--filter.PageNumber) * filter.PageSize);
                var query = await UnitOfWork.Repository.FindPagedAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
                var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.Permission>, IEnumerable<PermissionDto>>(query.Item2);
                return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));
            }
            catch (Exception e)
            {
                Result.Message = e.InnerException != null ? e.InnerException.Message : e.Message;
                Result = new ResponseResult(null, status: HttpStatusCode.InternalServerError, exception: e, message: Result.Message);
                return new DataPaging(0, 0, 0, Result);
            }
        }
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
    }
}
