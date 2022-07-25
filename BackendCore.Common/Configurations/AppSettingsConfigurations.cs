using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace BackendCore.Common.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class AppSettingsConfigurations
    {
        public static IConfigurationRoot ReadConfigurationFromAppSettings()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            return root;
        }
    }
}
