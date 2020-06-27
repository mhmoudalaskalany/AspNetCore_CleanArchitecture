using System.Collections.Generic;
using System.Threading.Tasks;
using BackendCore.Common.Core;

namespace BackendCore.Service.Services.Base
{
    public interface IBaseService<T, TDto, TGetDto>
    {
        Task<IResult> GetAllAsync(bool disableTracking = false);
        Task<IResult> AddAsync(TDto model);
        Task<IResult> AddListAsync(List<TDto> model);
        Task<IResult> UpdateAsync(TDto model);
        Task<IResult> DeleteAsync(long id);
        Task<IResult> GetByIdAsync(long id);
    }
}