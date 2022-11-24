using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Common.Extensions;

namespace Common.DTO.Base
{
    [ExcludeFromCodeCoverage]
    public class BaseParam<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public T Filter { get; set; }
        public IEnumerable<SortModel> OrderByValue { get; set; }
    }
}
