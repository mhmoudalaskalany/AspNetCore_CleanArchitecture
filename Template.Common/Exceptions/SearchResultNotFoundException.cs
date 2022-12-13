using System.Diagnostics.CodeAnalysis;

namespace Template.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class SearchResultNotFoundException :BaseException
    {
        public SearchResultNotFoundException():base("Result not found")
        {
                
        }
    }
}
