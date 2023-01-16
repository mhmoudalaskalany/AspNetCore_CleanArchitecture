using System;
using System.Diagnostics.CodeAnalysis;
using FluentScheduler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;
using Template.Application.Services.BackgroundJobs.Jobs;
using Template.Infrastructure.Context;
using Environment = Template.Common.StaticData.Environment;

namespace Template.Api.Extensions
{
    /// <summary>
    /// Pipeline Extensions
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ConfigureMiddlewareExtension
    {
        /// <summary>
        /// General Configuration Method
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder Configure(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.ConfigureCors();
            app.CreateDatabase();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.AddLocalization();
            app.UseFluentScheduler(configuration);
            app.SwaggerConfig(configuration);
            app.UseHealthChecks("/probe");
            return app;
        }
        /// <summary>
        /// Configure Cors
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureCors(this IApplicationBuilder app)
        {
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }

        /// <summary>
        /// Create Database From Migration
        /// </summary>
        /// <param name="app"></param>
        public static void CreateDatabase(this IApplicationBuilder app)
        {
            try
            {
                using var scope =
                    app.ApplicationServices.CreateScope();
                using var context = scope.ServiceProvider.GetService<BackendCoreDbContext>();
                context?.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        /// <summary>
        /// Add Localization
        /// </summary>
        /// <param name="app"></param>
        public static void AddLocalization(this IApplicationBuilder app)
        {
            var supportedCultures = new[] { "en-US", "ar-OM" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures()
                .AddSupportedUICultures();
            app.UseRequestLocalization(localizationOptions);
        }

        /// <summary>
        /// User Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        private static void SwaggerConfig(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string endPoint = configuration["SwaggerConfig:EndPoint"];
                string title = configuration["SwaggerConfig:Title"];
                c.SwaggerEndpoint(endPoint, title);
                c.DocumentTitle = $"{title} Documentation";
                c.DocExpansion(DocExpansion.None);
            });
        }

        /// <summary>
        /// User Fluent Scheduler
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void UseFluentScheduler(this IApplicationBuilder app, IConfiguration configuration)
        {
            var env = configuration["Environment"];
            if (env == Environment.Development)
            {
                JobManager.Initialize(new MyRegistry());
            }

        }
    }
}
