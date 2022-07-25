using System.Diagnostics.CodeAnalysis;
using BackendCore.Common.DTO.Base;

namespace BackendCore.Common.DTO.Lookup.Action.Parameters
{
    [ExcludeFromCodeCoverage]
    public class ActionFilter : MainFilter
    {
        public int? Id { get; set; }
    }
}
