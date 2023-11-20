using System;
using System.Diagnostics.CodeAnalysis;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Template.Api.Extensions;
using Template.Api.MiddleWares;
using Template.Api.MiddleWares.Swagger;
using Template.Application.DependencyExtension;

namespace Template.Api
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
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            _shell.ConfigureHttp(app, env);
            Shell.Start(_shell);
            
            app.Configure(env, Configuration , provider);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomMiddleware();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}