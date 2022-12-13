using System;
using FluentScheduler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Template.Application.DependencyExtension;

namespace Template.Application.Services.BackgroundJobs.Jobs
{
    public class TestJob : IJob
    {
        private readonly ILogger<TestJob> _logger;
        public TestJob()
        {
            Shell.RootInjector.GetService<IConfiguration>();
            _logger = Shell.RootInjector.GetRequiredService<ILogger<TestJob>>();
        }

        #region Public Methods

        public void ExecuteTestJob()
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
                _logger.LogError(exceptionJson);
            }
        }

        #endregion

        #region Private Methods



        #endregion



    }
}
