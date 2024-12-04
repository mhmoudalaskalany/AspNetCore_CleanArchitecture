using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Asp.Versioning.ApiExplorer;
using FluentScheduler;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using Template.Api.Authorization;
using Template.Application.Services.BackgroundJobs.Jobs;
using Template.Common.FeatureFlags;
using Template.Domain;
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
        public static IApplicationBuilder Configure(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration, IApiVersionDescriptionProvider provider)
        {
            app.ConfigureCors();
            app.CreateDatabase();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.AddLocalization();
            app.UseFluentScheduler(configuration);
            app.SwaggerConfig(provider);
            app.UseHangFire();
            app.UseHealthChecks("/probe");
            return app;
        }
        /// <summary>
        /// Configure Cors
        /// </summary>
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
        public static void CreateDatabase(this IApplicationBuilder app)
        {
            try
            {
                using var scope =
                    app.ApplicationServices.CreateScope();
                using var context = scope.ServiceProvider.GetService<TemplateDbContext>();

                var pendingMigrations = context.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    Log.Information(MessagesConstants.ApplyingNewMigrations);
                    context.Database.Migrate();
                }
                else
                {
                    Log.Information(MessagesConstants.NoNewMigrations);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Error Running Database Migrations");
                throw;
            }

        }
        /// <summary>
        /// Add Localization
        /// </summary>
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
        private static void SwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant()
                    );
                }
                options.DocExpansion(DocExpansion.None);
            });
        }

        /// <summary>
        /// User Fluent Scheduler
        /// </summary>
        public static void UseFluentScheduler(this IApplicationBuilder app, IConfiguration configuration)
        {
            var env = configuration["Environment"];
            if (env == Environment.Development)
            {
                JobManager.Initialize(new MyRegistry());
            }
        }

        /// <summary>
        /// Add Hang fire
        /// </summary>
        public static void UseHangFire(this IApplicationBuilder app)
        {
            var featureManager = app.ApplicationServices.GetRequiredService<IFeatureManager>();
            if (featureManager.IsEnabledAsync(nameof(FeatureFlags.EnableHangFire)).Result)
                app.UseHangfireDashboard("/hangfire", new DashboardOptions()
                {
                    Authorization = new[] { new HangFireAuthorizationFilter() }

                });
        }
    }
}
