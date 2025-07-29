using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Template.Common.Core
{
    /// <summary>
    /// Standard API response wrapper for paginated data
    /// </summary>
    /// <typeparam name="T">The type of the data being returned</typeparam>
    [ExcludeFromCodeCoverage]
    public class ApiPagedResponse<T> : ApiResponse<T>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public bool HasNextPage { get; set; }

        public bool HasPreviousPage { get; set; }

        public static ApiPagedResponse<T> SuccessResponse(
            T data,
            int pageNumber,
            int pageSize,
            int totalCount,
            string message = null,
            HttpStatusCode statusCode = HttpStatusCode.OK
        )
        {
            var totalPages = pageSize > 0 ? (int)Math.Ceiling((double)totalCount / pageSize) : 0;
            return new ApiPagedResponse<T>
            {
                Success = true,
                Data = data,
                Message = message,
                StatusCode = (int)statusCode,
                Errors = new List<string>(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasNextPage = pageNumber < totalPages,
                HasPreviousPage = pageNumber > 1,
                Timestamp = DateTime.UtcNow
            };
        }

        public static ApiPagedResponse<T> ErrorResponse(
            string message,
            int pageNumber = 1,
            int pageSize = 10,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest,
            IEnumerable<string> errors = null
        )
        {
            return new ApiPagedResponse<T>
            {
                Success = false,
                Data = default(T),
                Message = message,
                StatusCode = (int)statusCode,
                Errors = errors ?? new List<string>(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = 0,
                TotalPages = 0,
                HasNextPage = false,
                HasPreviousPage = false,
                Timestamp = DateTime.UtcNow
            };
        }

        public static ApiPagedResponse<T> ErrorResponse(
            IEnumerable<string> errors,
            int pageNumber = 1,
            int pageSize = 10,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest
        )
        {
            return new ApiPagedResponse<T>
            {
                Success = false,
                Data = default(T),
                Message = "One or more errors occurred",
                StatusCode = (int)statusCode,
                Errors = errors ?? new List<string>(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = 0,
                TotalPages = 0,
                HasNextPage = false,
                HasPreviousPage = false,
                Timestamp = DateTime.UtcNow
            };
        }
    }
}
