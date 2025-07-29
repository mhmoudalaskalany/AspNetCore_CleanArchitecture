using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System.Diagnostics.CodeAnalysis;
using Template.Common.Infrastructure.UnitOfWork;
using Template.Integration.CacheRepository;

namespace Template.Application.Services.Base
{
    [ExcludeFromCodeCoverage]
    public class ServiceBaseParameter<T> : IServiceBaseParameter<T> where T : class
    {

        public ServiceBaseParameter(
            IMapper mapper,
            IUnitOfWork<T> unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            ICacheRepository cacheRepository,
            IConfiguration configuration,
            ILogger<T> logger
        )
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
            HttpContextAccessor = httpContextAccessor;
            CacheRepository = cacheRepository;
            Configuration = configuration;
            Logger = logger;
        }

        public IMapper Mapper { get; set; }

        public IUnitOfWork<T> UnitOfWork { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public ICacheRepository CacheRepository { get; set; }

        public IConfiguration Configuration { get; set; }

        public ILogger<T> Logger { get; set; }

    }
}