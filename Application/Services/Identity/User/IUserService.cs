using System;
using System.Threading.Tasks;
using Application.Services.Base;
using Common.Core;
using Common.DTO.Base;
using Common.DTO.Identity.User;
using Common.DTO.Identity.User.Parameters;

namespace Application.Services.Identity.User
{
    public interface IUserService : IBaseService<Domain.Entities.Identity.User, AddUserDto, UserDto , Guid, Guid?>
    {
        /// <summary>
        /// Get All Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetAllPagedAsync(BaseParam<UserFilter> filter);
    }
}