﻿using System.Diagnostics.CodeAnalysis;
using System.Reflection;
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
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDbContext(configuration);
            services.AddLocalizationServices();
            services.RegisterCores();
            services.RegisterRepository();
            services.RegisterIntegrationRepositories();
            services.RegisterAutoMapper();
            services.RegisterCommonServices(configuration);
            services.AddHealthChecks();
            services.AddControllers();
            return services;
        }

        /// <summary>
        /// Add DbContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TemplateDbContextDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(ConnectionStringName));
            });
            services.AddScoped<DbContext, TemplateDbContextDbContext>();
            services.AddSingleton<IDataInitializer, DataInitializer>();
        }


        /// <summary>
        /// register auto-mapper
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingService));

        }

        /// <summary>
        /// Register localization
        /// </summary>
        /// <param name="services"></param>
        private static void AddLocalizationServices(this IServiceCollection services)
        {
            services.AddLocalization();
        }

        /// <summary>
        /// Register Custom Repositories
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped<IActiveDirectoryRepository, ActiveDirectoryRepository>();
        }

        /// <summary>
        /// register Integration Repositories
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterIntegrationRepositories(this IServiceCollection services)
        {
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<ICacheRepository, CacheRepository>();
        }

        /// <summary>
        /// Register Main Core
        /// </summary>
        /// <param name="services"></param>
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
    }
}
