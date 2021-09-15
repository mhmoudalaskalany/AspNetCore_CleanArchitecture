using System;
using BackendCore.Service.DependencyExtension;
using FluentScheduler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BackendCore.Service.Services.BackgroundJobs.Jobs
{
    public class TestJob : IJob
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TestJob> _logger;
        public TestJob()
        {
            _configuration = Shell.RootInjector.GetService<IConfiguration>();
            _logger = Shell.RootInjector.GetRequiredService<ILogger<TestJob>>();
        }

        #region Public Methods

        public async void ExecuteTestJob()
        {
        }

        public void Execute()
        {
            try
            {
                ExecuteTestJob();
            }
            catch (Exception e)
            {
                var serializerSettings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented
                };
                var exception = new
                {
                    e.Message,
                    e.StackTrace,
                    e.InnerException
                };
                var exceptionJson = JsonConvert.SerializeObject(exception, serializerSettings);
                Console.WriteLine(e);
            }
        }

        #endregion

        #region Private Methods



        #endregion



    }
}
