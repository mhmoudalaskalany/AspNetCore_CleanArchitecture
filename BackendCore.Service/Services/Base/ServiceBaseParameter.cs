using AutoMapper;
using BackendCore.Common.Abstraction.UnitOfWork;
using BackendCore.Common.Core;

namespace BackendCore.Service.Services.Base
{
    public class ServiceBaseParameter<T> : IServiceBaseParameter<T> where T : class
    {

        public ServiceBaseParameter(IMapper mapper, IUnitOfWork<T> unitOfWork, IResponseResult responseResult)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
            ResponseResult = responseResult;
        }

        public IMapper Mapper { get; set; }
        public IUnitOfWork<T> UnitOfWork { get; set; }
        public IResponseResult ResponseResult { get; set; }
    }
}