﻿using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Template.Common.Core;
using Template.Common.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Template.Integration.CacheRepository;

namespace Template.Application.Services.Base
{
    [ExcludeFromCodeCoverage]
    public class ServiceBaseParameter<T> : IServiceBaseParameter<T> where T : class
    {

        public ServiceBaseParameter(
            IMapper mapper,
            IUnitOfWork<T> unitOfWork,
            IResponseResult responseResult,
            IHttpContextAccessor httpContextAccessor,
            ICacheRepository cacheRepository,
            IConfiguration configuration
        )
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
            ResponseResult = responseResult;
            HttpContextAccessor = httpContextAccessor;
            CacheRepository = cacheRepository;
            Configuration = configuration;
        }

        public IMapper Mapper { get; set; }

        public IUnitOfWork<T> UnitOfWork { get; set; }

        public IResponseResult ResponseResult { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public ICacheRepository CacheRepository { get; set; }

        public IConfiguration Configuration { get; set; }

    }
}