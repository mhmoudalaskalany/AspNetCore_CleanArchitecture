using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Template.Common.Core;

namespace Template.Application.Services.Base
{
    public interface IBaseService<T, TAddDto , TEditDto, TGetDto, TKey , TKeyDto>
        where T : class
        where TAddDto : IEntityDto<TKeyDto>
        where TEditDto : IEntityDto<TKeyDto>
        where TGetDto : IEntityDto<TKeyDto>
    {
        Task<IFinalResult> GetByIdAsync(object id);

        Task<IFinalResult> GetEditByIdAsync(object id);

        Task<IFinalResult> GetAllAsync(bool disableTracking = false, Expression<Func<T, bool>> predicate = null);

        Task<IFinalResult> AddAsync(TAddDto model);

        Task<IFinalResult> AddListAsync(List<TAddDto> model);

        Task<IFinalResult> UpdateAsync(TAddDto model);

        Task<IFinalResult> DeleteAsync(object id);

        Task<IFinalResult> DeleteSoftAsync(object id);
        
    }
}