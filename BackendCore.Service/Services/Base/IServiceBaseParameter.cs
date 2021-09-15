using AutoMapper;
using BackendCore.Common.Core;
using BackendCore.Common.Infrastructure.UnitOfWork;
using BackendCore.Integration.CacheRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BackendCore.Service.Services.Base
{
    public interface IServiceBaseParameter<T> where T : class
    {
        IMapper Mapper { get; set; }
        IUnitOfWork<T> UnitOfWork { get; set; }
        IResponseResult ResponseResult { get; set; }
        IHttpContextAccessor HttpContextAccessor { get; set; }
        IConfiguration Configuration { get; set; }
        ICacheRepository CacheRepository { get; set; }
    }
}