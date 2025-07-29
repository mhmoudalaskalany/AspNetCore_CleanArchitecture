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
        Task<Result<TGetDto>> GetByIdAsync(object id);

        Task<Result<TEditDto>> GetEditByIdAsync(object id);

        Task<Result<IEnumerable<TGetDto>>> GetAllAsync(bool disableTracking = false, Expression<Func<T, bool>> predicate = null);

        Task<Result<TKeyDto>> AddAsync(TAddDto model);

        Task<Result<IEnumerable<TKeyDto>>> AddListAsync(List<TAddDto> model);

        Task<Result<TKeyDto>> UpdateAsync(TAddDto model);

        Task<Result> DeleteAsync(object id);

        Task<Result> DeleteSoftAsync(object id);


    }
}