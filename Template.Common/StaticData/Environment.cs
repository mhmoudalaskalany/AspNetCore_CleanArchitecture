using System.Diagnostics.CodeAnalysis;

namespace Template.Common.StaticData
{
    [ExcludeFromCodeCoverage]
    public static class Environment
    {
        public static string Development = "Dev";

        public static string Production = "Prod";
    }
}
