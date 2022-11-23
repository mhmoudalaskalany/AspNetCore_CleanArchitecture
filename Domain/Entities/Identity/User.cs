using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Entities.Base;

namespace Domain.Entities.Identity
{
    [ExcludeFromCodeCoverage]
    public class User : BaseEntity<Guid>
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string NationalId { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
