namespace BackendCore.Common.Core
{
    public class DataPaging
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public IResult Result { get; set; }
        public DataPaging(int pageNumber, int pageSize, int totalPage, IResult result)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPage = totalPage;
            Result = result;
        }
    }
}
