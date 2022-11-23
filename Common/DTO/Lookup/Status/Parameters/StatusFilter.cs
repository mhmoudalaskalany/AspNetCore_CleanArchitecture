using System.Diagnostics.CodeAnalysis;
using Common.DTO.Base;

namespace Common.DTO.Lookup.Status.Parameters
{
    [ExcludeFromCodeCoverage]
    public class StatusFilter : MainFilter
    {
        public int? Id { get; set; }
    }
}
