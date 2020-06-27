using BackendCore.Entities.Entities.Base;

namespace BackendCore.Entities.Entities
{
    public class Permission : BaseEntity
    {
        public long Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
    }
}