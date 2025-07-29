using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Template.Common.Core
{
    /// <summary>
    /// Standard API response wrapper for controllers
    /// </summary>
    /// <typeparam name="T">The type of the data being returned</typeparam>
    [ExcludeFromCodeCoverage]
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public int StatusCode { get; set; }

        public DateTime Timestamp { get; set; }

        public ApiResponse()
        {
            Timestamp = DateTime.UtcNow;
            Errors = new List<string>();
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message,
                StatusCode = (int)statusCode,
                Errors = new List<string>()
            };
        }

        public static ApiResponse<T> ErrorResponse(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest, IEnumerable<string> errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Data = default(T),
                Message = message,
                StatusCode = (int)statusCode,
                Errors = errors ?? new List<string>()
            };
        }

        public static ApiResponse<T> ErrorResponse(IEnumerable<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Data = default(T),
                Message = "One or more errors occurred",
                StatusCode = (int)statusCode,
                Errors = errors ?? new List<string>()
            };
        }
    }

    /// <summary>
    /// Standard API response wrapper for controllers without data
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ApiResponse : ApiResponse<object>
    {
        public static ApiResponse SuccessResponse(string message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiResponse
            {
                Success = true,
                Message = message,
                StatusCode = (int)statusCode,
                Errors = new List<string>()
            };
        }

        public new static ApiResponse ErrorResponse(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest, IEnumerable<string> errors = null)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message,
                StatusCode = (int)statusCode,
                Errors = errors ?? new List<string>()
            };
        }

        public new static ApiResponse ErrorResponse(IEnumerable<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ApiResponse
            {
                Success = false,
                Message = "One or more errors occurred",
                StatusCode = (int)statusCode,
                Errors = errors ?? new List<string>()
            };
        }
    }
}
