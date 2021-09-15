using System;
using BackendCore.Common.DTO.Base;

namespace BackendCore.Common.DTO.Identity.User.Parameters
{
    public class UserFilter : MainFilter
    {
        public Guid Id { get; set; }
    }
}
