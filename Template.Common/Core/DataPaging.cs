using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Template.Common.Core
{
    [ExcludeFromCodeCoverage]
    public class DataPaging
    {
        public PagingResult Data { get; set; }

        public HttpStatusCode Status { get; set; }

        public string Message { get; set; }

        public DataPaging(int pageNumber, int pageSize, int totalCount, object result, HttpStatusCode status, string message)
        {
            Data = new PagingResult(pageNumber, pageSize, totalCount, result);
            Status = status;
            Message = message;
        }
    }

    [ExcludeFromCodeCoverage]
    public class PagingResult
    {
        public PagingResult(int pageNumber, int pageSize, int totalCount, object result)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = result;
        }
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public object Data { get; set; }
    }
}