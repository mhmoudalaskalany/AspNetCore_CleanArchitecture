using System.Diagnostics.CodeAnalysis;
using Common.DTO.Base;

namespace Common.DTO.Lookup.Action.Parameters
{
    [ExcludeFromCodeCoverage]
    public class ActionFilter : MainFilter
    {
        public int? Id { get; set; }
    }
}
