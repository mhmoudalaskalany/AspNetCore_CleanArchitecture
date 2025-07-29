using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Template.Common.Infrastructure.UnitOfWork;
using Template.Integration.CacheRepository;

namespace Template.Application.Services.Base
{
    public interface IServiceBaseParameter<T> where T : class
    {
        IMapper Mapper { get; set; }

        IUnitOfWork<T> UnitOfWork { get; set; }

        IHttpContextAccessor HttpContextAccessor { get; set; }

        IConfiguration Configuration { get; set; }

        ICacheRepository CacheRepository { get; set; }
        ILogger<T> Logger { get; set; }
    }
}