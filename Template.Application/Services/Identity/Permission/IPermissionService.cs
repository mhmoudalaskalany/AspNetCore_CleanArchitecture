using System.Threading.Tasks;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.DTO.Identity.Permission;
using Template.Common.DTO.Identity.Permission.Parameters;
using Template.Application.Services.Base;
using System.Collections.Generic;

namespace Template.Application.Services.Identity.Permission
{
    public interface IPermissionService : IBaseService<Domain.Entities.Identity.Permission, AddPermissionDto , EditPermissionDto, PermissionDto , int , int?>
    {
        Task<PagedResult<IEnumerable<PermissionDto>>> GetAllPagedAsync(BaseParam<PermissionFilter> filter);

        Task<PagedResult<IEnumerable<PermissionDto>>> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter);

        Task<Result> DeleteRangeAsync(List<int> ids);
    }
}