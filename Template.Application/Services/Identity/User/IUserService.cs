using System;
using System.Threading.Tasks;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.DTO.Identity.User;
using Template.Common.DTO.Identity.User.Parameters;
using Template.Application.Services.Base;

namespace Template.Application.Services.Identity.User
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