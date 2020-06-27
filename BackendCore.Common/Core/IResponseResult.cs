using System;
using System.Net;

namespace BackendCore.Common.Core
{
    public interface IResponseResult : IResult
    {
        IResult PostResult(object result = null,
            HttpStatusCode status = HttpStatusCode.BadRequest, Exception exception = null,
            string message = null);
    }
}
