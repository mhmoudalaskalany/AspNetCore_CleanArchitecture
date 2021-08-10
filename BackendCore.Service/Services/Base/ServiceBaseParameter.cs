using AutoMapper;
using BackendCore.Common.Abstraction.UnitOfWork;
using BackendCore.Common.Core;
using Microsoft.AspNetCore.Http;

namespace BackendCore.Service.Services.Base
{
    public class ServiceBaseParameter<T> : IServiceBaseParameter<T> where T : class
    {

        public ServiceBaseParameter(
            IMapper mapper, 
            IUnitOfWork<T> unitOfWork, 
            IResponseResult responseResult,
            IHttpContextAccessor httpContextAccessor
            )
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
            ResponseResult = responseResult;
            HttpContextAccessor = httpContextAccessor;
        }

        public IMapper Mapper { get; set; }
        public IUnitOfWork<T> UnitOfWork { get; set; }
        public IResponseResult ResponseResult { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
    }
}