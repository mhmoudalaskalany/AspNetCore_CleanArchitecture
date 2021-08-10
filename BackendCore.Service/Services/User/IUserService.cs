﻿using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Common.DTO.Base;
using BackendCore.Common.DTO.User;
using BackendCore.Common.DTO.User.Parameters;
using BackendCore.Service.Services.Base;

namespace BackendCore.Service.Services.User
{
    public interface IUserService : IBaseService<Entities.Entities.User, AddUserDto, UserDto , long, long?>
    {
        /// <summary>
        /// Get All Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetAllPagedAsync(BaseParam<UserFilter> filter);
    }
}