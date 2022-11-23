using System.Diagnostics.CodeAnalysis;
using Common.DTO.Base;

namespace Common.DTO.Identity.Permission.Parameters
{
    [ExcludeFromCodeCoverage]
    public class PermissionFilter : MainFilter
    {
        public int? Id { get; set; }
    }
}
