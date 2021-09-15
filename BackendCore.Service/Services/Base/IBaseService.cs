using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BackendCore.Common.Core;

namespace BackendCore.Service.Services.Base
{
    public interface IBaseService<T, TDto, TGetDto, TKey , TKeyDto>
        where T : class
        where TDto : IEntityDto<TKeyDto>
        where TGetDto : IEntityDto<TKeyDto>
    {
        Task<IResult> GetAllAsync(bool disableTracking = false, Expression<Func<T, bool>> predicate = null);
        Task<IResult> AddAsync(TDto model);
        Task<IResult> AddListAsync(List<TDto> model);
        Task<IResult> UpdateAsync(TDto model);
        Task<IResult> DeleteAsync(long id);
        Task<IResult> DeleteSoftAsync(long id);
        Task<IResult> GetByIdAsync(long id);
    }
}