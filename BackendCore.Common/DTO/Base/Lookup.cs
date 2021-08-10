using BackendCore.Common.Core;

namespace BackendCore.Common.DTO.Base
{
    public class LookupDto<T> : IEntityDto<T>
    {
        public T Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
    }
}
