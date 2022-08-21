using System;
using System.Diagnostics.CodeAnalysis;
using BackendCore.Api.Extensions;
using BackendCore.Common.Exceptions;
using BackendCore.Common.MiddleWares;
using BackendCore.Service.DependencyExtension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackendCore.Api
{
    /// <summary>
    /// Start Up Class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly Shell _shell;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _shell = (Shell)Activator.CreateInstance(typeof(Shell));
        }
        /// <summary>
        /// Public Configuration Property
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure Dependencies
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServices(Configuration);

        }

        /// <summary>
        /// Configure Pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _shell.ConfigureHttp(app, env);
            Shell.Start(_shell);
            app.ConfigureCustomMiddleware();
            app.Configure(env, Configuration);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}