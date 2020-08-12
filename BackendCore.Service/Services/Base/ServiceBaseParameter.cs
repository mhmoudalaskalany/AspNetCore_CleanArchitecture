using AutoMapper;
using BackendCore.Common.Abstraction.UnitOfWork;
using BackendCore.Common.Core;
using BackendCore.Entities.Entities.Base;
using Microsoft.AspNetCore.Http;

namespace BackendCore.Service.Services.Base
{
    public class ServiceBaseParameter<T,TKey> : IServiceBaseParameter<T,TKey> where T : BaseEntity<TKey>
    {

        public ServiceBaseParameter(
            IMapper mapper, 
            IUnitOfWork<T,TKey> unitOfWork, 
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
        public IUnitOfWork<T, TKey> UnitOfWork { get; set; }
        public IResponseResult ResponseResult { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
    }
}