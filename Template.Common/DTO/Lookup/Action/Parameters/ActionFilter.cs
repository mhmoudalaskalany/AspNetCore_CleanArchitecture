using System.Diagnostics.CodeAnalysis;
using Template.Common.DTO.Base;

namespace Template.Common.DTO.Lookup.Action.Parameters
{
    [ExcludeFromCodeCoverage]
    public class ActionFilter : MainFilter
    {
        public int? Id { get; set; }
    }
}
