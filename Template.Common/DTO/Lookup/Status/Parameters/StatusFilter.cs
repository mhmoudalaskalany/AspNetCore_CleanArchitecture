using System.Diagnostics.CodeAnalysis;
using Template.Common.DTO.Base;

namespace Template.Common.DTO.Lookup.Status.Parameters
{
    [ExcludeFromCodeCoverage]
    public class StatusFilter : MainFilter
    {
        public int? Id { get; set; }
    }
}
