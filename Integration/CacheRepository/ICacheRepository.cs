using System.Threading.Tasks;

namespace Integration.CacheRepository
{
    public interface ICacheRepository
    {
        /// <summary>
        /// Get Employee From Cache By National Id
        /// </summary>
        /// <param name="nationalId"></param>
        /// <returns></returns>
        Task<object> GetEmployeeAsync(string nationalId);
    }
}
