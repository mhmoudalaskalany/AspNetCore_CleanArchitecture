using System;
using System.Diagnostics.CodeAnalysis;
using Common.DTO.Base;

namespace Common.DTO.Identity.User.Parameters
{
    [ExcludeFromCodeCoverage]
    public class UserFilter : MainFilter
    {
        public Guid Id { get; set; }
    }
}
