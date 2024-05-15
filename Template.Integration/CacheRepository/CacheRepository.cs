using System.Threading.Tasks;
using Template.Common.Caching.Redis;
using Template.Common.Helpers.HttpClient.RestSharp;

namespace Template.Integration.CacheRepository
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IRestSharpClient _restSharpClient;
        public CacheRepository(IRestSharpClient restSharpClient)
        {
            _restSharpClient = restSharpClient;
        }

        public async Task<object> GetEmployeeAsync(string nationalId)
        {
            var employee = RedisCacheHelper.GetT<object>(nationalId);
            return employee;
        }


    }
}
