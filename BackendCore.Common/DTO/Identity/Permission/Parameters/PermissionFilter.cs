using System.Diagnostics.CodeAnalysis;
using BackendCore.Common.DTO.Base;

namespace BackendCore.Common.DTO.Identity.Permission.Parameters
{
    [ExcludeFromCodeCoverage]
    public class PermissionFilter : MainFilter
    {
        public int? Id { get; set; }
    }
}
