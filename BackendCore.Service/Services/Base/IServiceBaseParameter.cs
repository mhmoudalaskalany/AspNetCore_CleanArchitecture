using AutoMapper;
using BackendCore.Common.Abstraction.UnitOfWork;
using BackendCore.Common.Core;

namespace BackendCore.Service.Services.Base
{
    public interface IServiceBaseParameter<T> where T : class
    {
        IMapper Mapper { get; set; }
        IUnitOfWork<T> UnitOfWork { get; set; }
        IResponseResult ResponseResult { get; set; }
    }
}