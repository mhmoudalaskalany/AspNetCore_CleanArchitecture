using BackendCore.Entities.Entities.Base;

namespace BackendCore.Entities.Entities
{
    public class User : BaseEntity
    {
        public long Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string NationalId { get; set; }
        public long RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
