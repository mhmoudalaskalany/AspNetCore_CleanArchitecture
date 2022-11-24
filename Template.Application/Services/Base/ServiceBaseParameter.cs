﻿using AutoMapper;
using Common.Core;
using Common.Infrastructure.UnitOfWork;
using Integration.CacheRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Base
{
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