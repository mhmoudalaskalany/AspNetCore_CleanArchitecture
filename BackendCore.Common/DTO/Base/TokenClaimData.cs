using BackendCore.Entities.Enum;

namespace BackendCore.Common.DTO.Base
{
    public class TokenClaimDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public long UserTypeId { get; set; }
        public UserType UserType { get; set; }
        public string IpAddress { get; set; }

    }
}
