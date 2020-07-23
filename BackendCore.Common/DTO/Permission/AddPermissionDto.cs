using BackendCore.Common.Core;

namespace BackendCore.Common.DTO.Permission
{
    public class AddPermissionDto : IEntityDto<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
    }
}
