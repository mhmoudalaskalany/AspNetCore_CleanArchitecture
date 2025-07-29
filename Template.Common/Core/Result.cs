using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Template.Common.Core
{
    /// <summary>
    /// Represents the result of an operation without a return value
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Result
    {
        protected Result(bool isSuccess, string message = null, IEnumerable<string> errors = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Errors = errors ?? new List<string>();
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public string Message { get; }

        public IEnumerable<string> Errors { get; }

        public static Result Success(string message = null) => new(true, message);

        public static Result Failure(string message, IEnumerable<string> errors = null) => new(false, message, errors);

        public static Result Failure(IEnumerable<string> errors) => new(false, null, errors);
    }

    /// <summary>
    /// Represents the result of an operation with a return data
    /// </summary>
    /// <typeparam name="T">The type of the return value</typeparam>
    [ExcludeFromCodeCoverage]
    public class Result<T> : Result
    {
        protected Result(bool isSuccess, T data, string message = null, IEnumerable<string> errors = null) : base(isSuccess, message, errors)
        {
            Data = data;
        }

        public T Data { get; }

        public static Result<T> Success(T value, string message = null) => new(true, value, message);

        public new static Result<T> Failure(string message, IEnumerable<string> errors = null) => new(false, default(T), message, errors);

        public new static Result<T> Failure(IEnumerable<string> errors) => new(false, default(T), null, errors);
    }
}
