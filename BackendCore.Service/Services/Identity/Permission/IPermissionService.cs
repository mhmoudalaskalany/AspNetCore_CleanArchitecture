using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Base;
using BackendCore.Common.DTO.Identity.Permission;
using BackendCore.Common.DTO.Identity.Permission.Parameters;
using BackendCore.Service.Services.Base;

namespace BackendCore.Service.Services.Identity.Permission
{
    public interface IPermissionService : IBaseService<Entities.Entities.Identity.Permission, AddPermissionDto, PermissionDto , long , long?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<PermissionFilter> filter);
    }
}