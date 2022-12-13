using System.Diagnostics.CodeAnalysis;
using Template.Common.DTO.Base;

namespace Template.Common.DTO.Identity.Permission.Parameters
{
    [ExcludeFromCodeCoverage]
    public class PermissionFilter : MainFilter
    {
        public int? Id { get; set; }
    }
}
