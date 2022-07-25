using System.Diagnostics.CodeAnalysis;

namespace BackendCore.Common.StaticData
{
    [ExcludeFromCodeCoverage]
    public static class Environment
    {
        public static string Development = "Dev";

        public static string Production = "Prod";
    }
}
