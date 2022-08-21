using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using BackendCore.Common.Extensions;
using BackendCore.Common.Infrastructure.Repository.ActiveDirectory;
using BackendCore.Common.Infrastructure.UnitOfWork;
using BackendCore.Data.Context;
using BackendCore.Data.DataInitializer;
using BackendCore.Data.Repository.ActiveDirectory;
using BackendCore.Data.UnitOfWork;
using BackendCore.Integration.CacheRepository;
using BackendCore.Integration.FileRepository;
using BackendCore.Service.Helper;
using BackendCore.Service.Mapping;
using BackendCore.Service.Services.Base;
using BackendCore.Service.Services.Identity.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace BackendCore.Api.Extensions
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
            services.AddDbContext<BackendCoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(ConnectionStringName));
            });
            services.AddScoped<DbContext, BackendCoreDbContext>();
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
            services.AddTransient(typeof(IBaseService<,,,,>), typeof(BaseService<,,,,>));
            services.AddTransient(typeof(IServiceBaseParameter<>), typeof(ServiceBaseParameter<>));
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            var servicesToScan = Assembly.GetAssembly(typeof(PermissionService)); //..or whatever assembly you need
            services.RegisterAssemblyPublicNonGenericClasses(servicesToScan)
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();
        }
    }
}
