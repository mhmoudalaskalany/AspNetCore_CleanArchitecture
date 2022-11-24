using System.Threading.Tasks;
using Application.Services.Base;
using Common.Core;
using Common.DTO.Base;
using Common.DTO.Identity.Permission;
using Common.DTO.Identity.Permission.Parameters;

namespace Application.Services.Identity.Permission
{
    public interface IPermissionService : IBaseService<Domain.Entities.Identity.Permission, AddPermissionDto, PermissionDto , int , int?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<PermissionFilter> filter);
    }
}