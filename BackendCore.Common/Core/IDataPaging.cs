namespace BackendCore.Common.Core
{
    public interface IDataPaging
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        int TotalPage { get; set; }
        IResult Result { get; set; }
    }
}
