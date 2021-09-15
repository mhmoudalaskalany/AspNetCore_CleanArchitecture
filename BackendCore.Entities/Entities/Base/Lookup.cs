namespace BackendCore.Entities.Entities.Base
{
    public class Lookup<TKey> : BaseEntity<TKey>
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
    }
}
