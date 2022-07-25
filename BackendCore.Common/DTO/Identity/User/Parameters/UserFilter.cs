using System;
using System.Diagnostics.CodeAnalysis;
using BackendCore.Common.DTO.Base;

namespace BackendCore.Common.DTO.Identity.User.Parameters
{
    [ExcludeFromCodeCoverage]
    public class UserFilter : MainFilter
    {
        public Guid Id { get; set; }
    }
}
