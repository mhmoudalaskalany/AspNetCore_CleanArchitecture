using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Asp.Versioning;
using Template.Common.Extensions;
using Template.Common.Infrastructure.Repository.ActiveDirectory;
using Template.Common.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using Template.Application.Helper;
using Template.Application.Mapping;
using Template.Application.Services.Base;
using Template.Application.Services.Identity.Permission;
using Template.Infrastructure.Context;
using Template.Infrastructure.DataInitializer;
using Template.Infrastructure.Repository.ActiveDirectory;
using Template.Infrastructure.UnitOfWork;
using Template.Integration.CacheRepository;
using Template.Integration.FileRepository;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using System.IO;
using System;
using Microsoft.AspNetCore.Routing;
using Template.Api.Extensions.Swagger.Headers;
using Template.Api.Extensions.Swagger.Options;
using Hangfire;
using Microsoft.FeatureManagement;
using Template.Common.FeatureFlags;

namespace Template.Api.Extensions
{
    /// <summary>
    /// Dependency Extensions
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ConfigureDependencyExtension
    {
        private const string ConnectionStringName = "Default";
        /// <summary>
        /// Register Extensions
        /// </summary>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDbContext(configuration);
            services.AddFeatureManagement();
            services.AddLocalizationServices();
            services.RegisterCores();
            services.RegisterRepository();
            services.RegisterIntegrationRepositories();
            services.RegisterAutoMapper();
            services.RegisterCommonServices(configuration);
            services.RegisterApiMonitoring();
            services.RegisterApiVersioning();
            services.RegisterSwaggerConfig();
            services.RegisterLowerCaseUrls();
            services.RegisterHangFire(configuration);
            services.AddControllers();
            return services;
        }

        /// <summary>
        /// Add DbContext
        /// </summary>
        private static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TemplateDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(ConnectionStringName));
            });
            services.AddScoped<DbContext, TemplateDbContext>();
            services.AddSingleton<IDataInitializer, DataInitializer>();
        }

        /// <summary>
        /// Add Health Checks
        /// </summary>
        private static void RegisterApiMonitoring(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<TemplateDbContext>();
        }


        /// <summary>
        /// register auto-mapper
        /// </summary>
        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingService));

        }

        /// <summary>
        /// Register localization
        /// </summary>
        private static void AddLocalizationServices(this IServiceCollection services)
        {
            services.AddLocalization();
        }

        /// <summary>
        /// Register Custom Repositories
        /// </summary>
        private static void RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped<IActiveDirectoryRepository, ActiveDirectoryRepository>();
        }

        /// <summary>
        /// register Integration Repositories
        /// </summary>
        private static void RegisterIntegrationRepositories(this IServiceCollection services)
        {
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<ICacheRepository, CacheRepository>();
        }

        /// <summary>
        /// Register Api Versioning
        /// </summary>
        private static void RegisterApiVersioning(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            })
            .AddApiExplorer(config =>
            {
                config.GroupNameFormat = "'v'VVV";
                config.SubstituteApiVersionInUrl = true;
            });
        }


        /// <summary>
        /// Lower Case Urls
        /// </summary>
        private static void RegisterLowerCaseUrls(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
        }

        /// <summary>
        /// Swagger Config
        /// </summary>

        private static void RegisterSwaggerConfig(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
            services.AddSwaggerGen(options =>
            {
                var security = new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] { }

                            }
                        };
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(security);
                options.OperationFilter<LanguageHeader>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        /// <summary>
        /// Register Core Dependencies
        /// </summary>
        private static void RegisterCores(this IServiceCollection services)
        {
            services.AddSingleton<AppHelper>();
            services.AddTransient(typeof(IBaseService<,,,,,>), typeof(BaseService<,,,,,>));
            services.AddTransient(typeof(IServiceBaseParameter<>), typeof(ServiceBaseParameter<>));
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            var servicesToScan = Assembly.GetAssembly(typeof(PermissionService)); //..or whatever assembly you need
            services.RegisterAssemblyPublicNonGenericClasses(servicesToScan)
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();
        }

        /// <summary>
        /// Register Hang Fire
        /// </summary>
        private static void RegisterHangFire(this IServiceCollection services, IConfiguration configuration)
        {
            var featureManager = services.BuildServiceProvider().GetRequiredService<IFeatureManager>();
            if (featureManager.IsEnabledAsync(nameof(FeatureFlags.EnableHangFire)).Result)
            {
                var connection = configuration.GetConnectionString(ConnectionStringName);
                services.AddHangfire(config => config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(connection));
                services.AddHangfireServer();
            }

        }
    }
}
