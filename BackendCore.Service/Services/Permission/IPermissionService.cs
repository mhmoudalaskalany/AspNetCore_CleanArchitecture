using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Base;
using BackendCore.Common.DTO.Permission;
using BackendCore.Common.DTO.Permission.Parameters;
using BackendCore.Service.Services.Base;
namespace BackendCore.Service.Services.Permission
{
    public interface IPermissionService : IBaseService<Entities.Entities.Permission, AddPermissionDto, PermissionDto , long , long?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<PermissionFilter> filter);
    }
}