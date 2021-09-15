using System.ComponentModel.DataAnnotations;
using BackendCore.Common.Core;

namespace BackendCore.Common.DTO.Base
{
    public class LookupDto<T> : IEntityDto<T>
    {
        public T Id { get; set; }
        [Required]
        public string NameEn { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
