using System;

namespace BackendCore.Common.DTO.Identity.Login
{
    public class UserLoginReturn
    {
        public string Token { get; set; }
        public DateTime TokenValidTo { get; set; }
        public object UserId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
    }
}