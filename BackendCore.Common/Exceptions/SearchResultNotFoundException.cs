namespace BackendCore.Common.Exceptions
{
    public class SearchResultNotFoundException :BaseException
    {
        public SearchResultNotFoundException():base("Result not found")
        {
                
        }
    }
}
