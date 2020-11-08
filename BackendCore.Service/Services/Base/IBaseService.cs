using System.Collections.Generic;
using System.Threading.Tasks;
using BackendCore.Common.Core;
using BackendCore.Entities.Entities.Base;

namespace BackendCore.Service.Services.Base
{
    public interface IBaseService<T, TDto, TGetDto, TKey , TKeyDto>
        where T : class
        where TDto : IEntityDto<TKeyDto>
        where TGetDto : IEntityDto<TKeyDto>
    {
        Task<IResult> GetAllAsync(bool disableTracking = false);
        Task<IResult> AddAsync(TDto model);
        Task<IResult> AddListAsync(List<TDto> model);
        Task<IResult> UpdateAsync(TDto model);
        Task<IResult> DeleteAsync(long id);
        Task<IResult> DeleteSoftAsync(long id);
        Task<IResult> GetByIdAsync(long id);
    }
}