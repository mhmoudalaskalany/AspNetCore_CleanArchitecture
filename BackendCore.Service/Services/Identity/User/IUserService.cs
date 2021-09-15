using System;
using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Base;
using BackendCore.Common.DTO.Identity.User;
using BackendCore.Common.DTO.Identity.User.Parameters;
using BackendCore.Service.Services.Base;

namespace BackendCore.Service.Services.Identity.User
{
    public interface IUserService : IBaseService<Entities.Entities.Identity.User, AddUserDto, UserDto , Guid, Guid?>
    {
        /// <summary>
        /// Get All Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetAllPagedAsync(BaseParam<UserFilter> filter);
    }
}