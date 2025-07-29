using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Application.Services.Base;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.DTO.Identity.User;
using Template.Common.DTO.Identity.User.Parameters;

namespace Template.Application.Services.Identity.User
{
    public interface IUserService : IBaseService<Domain.Entities.Identity.User, AddUserDto , EditUserDto, UserDto , Guid, Guid?>
    {
        Task<PagedResult<IEnumerable<UserDto>>> GetAllPagedAsync(BaseParam<UserFilter> filter);

        Task<PagedResult<IEnumerable<UserDto>>> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter);

        Task<Result> DeleteRangeAsync(List<Guid> ids);
    }
}