using System.Diagnostics.CodeAnalysis;

namespace BackendCore.Common.DTO.Base
{
    [ExcludeFromCodeCoverage]
    public class MainFilter
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
