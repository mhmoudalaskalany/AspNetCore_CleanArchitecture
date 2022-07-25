using System.Diagnostics.CodeAnalysis;

namespace BackendCore.Entities.Entities.Base
{
    [ExcludeFromCodeCoverage]
    public class Lookup<TKey> : BaseEntity<TKey>
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
    }
}
