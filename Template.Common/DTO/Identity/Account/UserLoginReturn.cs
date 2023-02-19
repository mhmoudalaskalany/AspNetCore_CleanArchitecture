using System;
using System.Diagnostics.CodeAnalysis;

namespace Template.Common.DTO.Identity.Account
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