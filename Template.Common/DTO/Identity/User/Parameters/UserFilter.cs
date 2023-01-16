using System;
using System.Diagnostics.CodeAnalysis;
using Template.Common.DTO.Base;

namespace Template.Common.DTO.Identity.User.Parameters
{
    [ExcludeFromCodeCoverage]
    public class UserFilter : MainFilter
    {
        public Guid Id { get; set; }
    }
}
