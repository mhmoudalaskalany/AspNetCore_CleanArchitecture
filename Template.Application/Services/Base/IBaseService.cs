using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Template.Common.Core;

namespace Template.Application.Services.Base
{
    public interface IBaseService<T, TDto, TGetDto, TKey , TKeyDto>
        where T : class
        where TDto : IEntityDto<TKeyDto>
        where TGetDto : IEntityDto<TKeyDto>
    {
        Task<IFinalResult> GetByIdAsync(object id);

        Task<IFinalResult> GetAllAsync(bool disableTracking = false, Expression<Func<T, bool>> predicate = null);

        Task<IFinalResult> AddAsync(TDto model);

        Task<IFinalResult> AddListAsync(List<TDto> model);

        Task<IFinalResult> UpdateAsync(TDto model);

        Task<IFinalResult> DeleteAsync(object id);

        Task<IFinalResult> DeleteSoftAsync(object id);
        
    }
}