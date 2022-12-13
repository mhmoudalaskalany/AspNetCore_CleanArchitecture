using System.Threading.Tasks;
using Common.Core;
using Common.DTO.Base;
using Common.DTO.Identity.Permission;
using Common.DTO.Identity.Permission.Parameters;
using Template.Application.Services.Base;

namespace Template.Application.Services.Identity.Permission
{
    public interface IPermissionService : IBaseService<Domain.Entities.Identity.Permission, AddPermissionDto, PermissionDto , int , int?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<PermissionFilter> filter);
    }
}