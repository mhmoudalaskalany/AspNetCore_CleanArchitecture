using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BackendCore.Api
{
    /// <summary>
    /// Start Point
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Kick Off
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// Build Web Host
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
