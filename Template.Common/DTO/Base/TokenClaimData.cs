using System.Diagnostics.CodeAnalysis;
using Template.Domain.Enum;

namespace Template.Common.DTO.Base
{
    [ExcludeFromCodeCoverage]
    public class TokenClaimDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public long UserTypeId { get; set; }
        public UserType UserType { get; set; }
        public string IpAddress { get; set; }

    }
}
