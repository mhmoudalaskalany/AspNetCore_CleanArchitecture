using System.Diagnostics.CodeAnalysis;
using BackendCore.Common.DTO.Base;

namespace BackendCore.Common.DTO.Lookup.Status.Parameters
{
    [ExcludeFromCodeCoverage]
    public class StatusFilter : MainFilter
    {
        public int? Id { get; set; }
    }
}
