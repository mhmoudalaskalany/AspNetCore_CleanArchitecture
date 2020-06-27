using System;

namespace BackendCore.Common.DTO.Login
{
    public class UserLoginReturn
    {
        public string Token { get; set; }
        public DateTime TokenValidTo { get; set; }
        public long? UserId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
    }
}