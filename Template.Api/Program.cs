using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Template.Common.Extensions;

namespace Template.Api
{
    /// <summary>
    /// Start Point
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Program
    {
        /// <summary>
        /// Configuration Properties
        /// </summary>
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        /// <summary>
        /// Kick Off
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var applicationVersion = GetApplicationVersion();
            var applicationName = Configuration["ApplicationName"];
            Log.Logger = BaseLoggerConfiguration
                .CreateLoggerConfiguration(applicationName, applicationVersion)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .WriteToSql(Configuration["LoggingDbConnectionString"])
                .CreateLogger();

            try
            {
                Log.Information("-----Starting web host at {0} Api------", applicationName + "-" + applicationVersion);
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Host {0} terminated unexpectedly", applicationName + "-" + applicationVersion);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        /// <summary>
        /// Web Host Builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();

        private static string GetApplicationVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            if (version != null)
            {
                var plusIndex = version.IndexOf('+');
                if (plusIndex > 0)
                {
                    version = version.Substring(0, plusIndex);
                }
            }
            return version ?? "1.0.0"; // Default version if not found
        }
    }
}
