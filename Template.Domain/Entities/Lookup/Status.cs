using System.Diagnostics.CodeAnalysis;
using Template.Domain.Entities.Base;

namespace Template.Domain.Entities.Lookup
{
    [ExcludeFromCodeCoverage]
    public class Status : Lookup<int>
    {
        public string EntityName { get; set; }
        
        public string CssClass { get; set; }
    }
}
