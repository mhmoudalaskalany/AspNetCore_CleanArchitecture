using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Template.Common.Core
{
    /// <summary>
    /// Represents the result of a paginated operation with return data
    /// </summary>
    /// <typeparam name="T">The type of the return value</typeparam>
    [ExcludeFromCodeCoverage]
    public class PagedResult<T> : Result<T>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public bool HasNextPage { get; }
        public bool HasPreviousPage { get; }

        protected PagedResult(
            bool isSuccess,
            T data,
            int pageNumber,
            int pageSize,
            int totalCount,
            string message = null,
            IEnumerable<string> errors = null
        )
            : base(isSuccess, data, message, errors)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = pageSize > 0 ? (int)Math.Ceiling((double)totalCount / pageSize) : 0;
            HasNextPage = pageNumber < TotalPages;
            HasPreviousPage = pageNumber > 1;
        }

        public static PagedResult<T> Success(
            T data,
            int pageNumber,
            int pageSize,
            int totalCount,
            string message = null
        ) => new(true, data, pageNumber, pageSize, totalCount, message);

        public static PagedResult<T> Failure(
            string message,
            int pageNumber = 1,
            int pageSize = 10,
            IEnumerable<string> errors = null
        ) => new(false, default(T), pageNumber, pageSize, 0, message, errors);

        public static PagedResult<T> Failure(
            IEnumerable<string> errors,
            int pageNumber = 1,
            int pageSize = 10
        ) => new(false, default(T), pageNumber, pageSize, 0, null, errors);
    }
}
