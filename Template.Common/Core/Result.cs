using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Common.Core
{
    [ExcludeFromCodeCoverage]
    public class Result : IFinalResult
    {
        public object Data { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
