using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using BackendCore.Entities.Entities.Base;

namespace BackendCore.Entities.Entities.Identity
{
    [ExcludeFromCodeCoverage]
    public class Role : BaseEntity<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public virtual  ICollection<User> Users{ get; set; } = new Collection<User>();
    }
}
