namespace BackendCore.Common.Core
{
    public class DataPaging
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IResult Result { get; set; }
        public DataPaging(int pageNumber, int pageSize, int totalCount, IResult result)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Result = result;
        }
    }
}
