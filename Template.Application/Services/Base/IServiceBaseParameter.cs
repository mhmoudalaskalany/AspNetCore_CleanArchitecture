using AutoMapper;
using Template.Common.Core;
using Template.Common.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Template.Integration.CacheRepository;
using Microsoft.Extensions.Logging;

namespace Template.Application.Services.Base
{
    public interface IServiceBaseParameter<T> where T : class
    {
        IMapper Mapper { get; set; }
        IUnitOfWork<T> UnitOfWork { get; set; }
        IResponseResult ResponseResult { get; set; }
        IHttpContextAccessor HttpContextAccessor { get; set; }
        IConfiguration Configuration { get; set; }
        ICacheRepository CacheRepository { get; set; }
        ILogger<T> Logger { get; set; }
    }
}