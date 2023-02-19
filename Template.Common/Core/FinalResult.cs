using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Template.Common.Core
{
    [ExcludeFromCodeCoverage]
    public class FinalResult : IFinalResult
    {
        public object Data { get; set; }

        public HttpStatusCode Status { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }
    }
}
