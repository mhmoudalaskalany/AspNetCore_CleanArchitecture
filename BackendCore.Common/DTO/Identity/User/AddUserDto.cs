using System;
using System.Diagnostics.CodeAnalysis;
using BackendCore.Common.Core;

namespace BackendCore.Common.DTO.Identity.User
{
    [ExcludeFromCodeCoverage]
    public class AddUserDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string NationalId { get; set; }
    }
}
