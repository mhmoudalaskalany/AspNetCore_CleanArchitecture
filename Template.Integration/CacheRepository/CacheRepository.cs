using System.Threading.Tasks;
using Common.Caching.Redis;
using Integration.CacheRepository;
using Template.Common.Helpers.HttpClient.RestSharp;

namespace Template.Integration.CacheRepository
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IRestSharpContainer _restSharpContainer;
        public CacheRepository(IRestSharpContainer restSharpContainer)
        {
            _restSharpContainer = restSharpContainer;
        }

        #region Public Methods
        /// <summary>
        /// Get Employee From Cache By National Id
        /// </summary>
        /// <param name="nationalId"></param>
        /// <returns></returns>
        public async Task<object> GetEmployeeAsync(string nationalId)
        {
            var employee = RedisCacheHelper.GetT<object>(nationalId);
            return employee;
        }

        #endregion
    }
}
