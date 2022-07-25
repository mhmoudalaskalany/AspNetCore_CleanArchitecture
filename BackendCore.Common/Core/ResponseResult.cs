﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace BackendCore.Common.Core
{
    [ExcludeFromCodeCoverage]
    public class ResponseResult : Result, IResponseResult
    {
        public ResponseResult(object result = null, HttpStatusCode status = HttpStatusCode.BadRequest, Exception exception = null, string message = null)
        {
            Data = result;
            Exception = exception;
            Message = message;
            Status = status;
        }

        public IFinalResult PostResult(object result = null, HttpStatusCode status = HttpStatusCode.BadRequest, Exception exception = null, string message = null)
        {
            return new ResponseResult(result: result, status: status, exception: exception, message: message);
        }

    }
}
