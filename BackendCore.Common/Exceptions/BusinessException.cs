using System;
using System.Diagnostics.CodeAnalysis;

namespace BackendCore.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class BusinessException : BaseException
    {

        public BusinessException() : base($"Error From Server")
        {

        }
        public BusinessException(string message) : base(message)
        {

        }

        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {

        }

        
    }
}
