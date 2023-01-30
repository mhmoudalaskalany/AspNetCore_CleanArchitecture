using System.Diagnostics.CodeAnalysis;
using Template.Common.DTO.Base;

namespace Template.Common.DTO.Lookup.Category.Parameters
{
    [ExcludeFromCodeCoverage]
    public class CategoryFilter : MainFilter
    {
        public int? Id { get; set; }
    }
}
