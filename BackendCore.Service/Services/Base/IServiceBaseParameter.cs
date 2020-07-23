using AutoMapper;
using BackendCore.Common.Abstraction.UnitOfWork;
using BackendCore.Common.Core;
using BackendCore.Entities.Entities.Base;

namespace BackendCore.Service.Services.Base
{
    public interface IServiceBaseParameter<T,TKey> where T : BaseEntity<TKey>
    {
        IMapper Mapper { get; set; }
        IUnitOfWork<T,TKey> UnitOfWork { get; set; }
        IResponseResult ResponseResult { get; set; }
    }
}