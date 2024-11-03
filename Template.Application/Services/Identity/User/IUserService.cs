using System;
using System.Threading.Tasks;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.DTO.Identity.User;
using Template.Common.DTO.Identity.User.Parameters;
using Template.Application.Services.Base;
using System.Collections.Generic;

namespace Template.Application.Services.Identity.User
{
    public interface IUserService : IBaseService<Domain.Entities.Identity.User, AddUserDto , EditUserDto, UserDto , Guid, Guid?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<UserFilter> filter);

        Task<DataPaging> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter);

        Task<IFinalResult> DeleteRangeAsync(List<Guid> ids);
    }
}