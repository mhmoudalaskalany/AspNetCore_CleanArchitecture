using BackendCore.Common.Core;

namespace BackendCore.Common.DTO.Permission
{
    public class PermissionDto : IEntityDto<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public bool? IsSelected { get; set; }
    }
}
