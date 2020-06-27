namespace BackendCore.Common.Extensions
{
    public class SortModel
    {
        public string ColId { get; set; } = "id";
        public string Sort { get; set; } = "asc";
        public string PairAsSqlExpression
        {
            get
            {
                return $"{ColId} {Sort}";
            }
        }
    }
}
