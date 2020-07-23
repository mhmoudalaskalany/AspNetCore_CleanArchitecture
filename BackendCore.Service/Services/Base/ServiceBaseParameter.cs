using AutoMapper;
using BackendCore.Common.Abstraction.UnitOfWork;
using BackendCore.Common.Core;
using BackendCore.Entities.Entities.Base;

namespace BackendCore.Service.Services.Base
{
    public class ServiceBaseParameter<T,TKey> : IServiceBaseParameter<T,TKey> where T : BaseEntity<TKey>
    {

        public ServiceBaseParameter(IMapper mapper, IUnitOfWork<T,TKey> unitOfWork, IResponseResult responseResult)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
            ResponseResult = responseResult;
        }

        public IMapper Mapper { get; set; }
        public IUnitOfWork<T, TKey> UnitOfWork { get; set; }
        public IResponseResult ResponseResult { get; set; }
    }
}