using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BackendCore.Entities.Entities.Base;

namespace BackendCore.Entities.Entities
{
    public class Role : BaseEntity<long>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public virtual  ICollection<User> Users{ get; set; } = new Collection<User>();
    }
}
