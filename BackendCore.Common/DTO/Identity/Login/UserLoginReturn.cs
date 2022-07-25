using System;
using System.Diagnostics.CodeAnalysis;

namespace BackendCore.Common.DTO.Identity.Login
{
    [ExcludeFromCodeCoverage]
    public class UserLoginReturn
    {
        public string Token { get; set; }
        public DateTime TokenValidTo { get; set; }
        public object UserId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
    }
}