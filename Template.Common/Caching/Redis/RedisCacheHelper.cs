using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using Template.Common.Configurations;

namespace Template.Common.Caching.Redis
{
    public static class RedisCacheHelper
    {
        // one reused ConnectionMultiplexer (StackExchange.Redis is built to be shared, never reconnected per call) —
        // this was the R1 leak. PublicationOnly so a connect failure isn't cached: we reuse on success, retry on outage.
        // Move to a DI-registered IConnectionMultiplexer singleton if this helper ever needs mocking/deterministic disposal.
        private static readonly Lazy<IConfigurationRoot> Configuration =
            new(AppSettingsConfigurations.ReadConfigurationFromAppSettings);

        private static readonly Lazy<ConnectionMultiplexer> Multiplexer =
            new(() => ConnectionMultiplexer.Connect(Configuration.Value.GetSection("RedisConfig")["StackExchangeConnection"]),
                LazyThreadSafetyMode.PublicationOnly);

        public static bool Set(string key, object value, string databaseIdKey = null)
        {
            GetDatabase(databaseIdKey).StringSet(key, JsonConvert.SerializeObject(value));
            return true;
        }

        public static void Set(string key, object value, int time, string databaseIdKey = null)
        {
            var timeInSeconds = TimeSpan.FromHours(time);
            GetDatabase(databaseIdKey).StringSet(key, JsonConvert.SerializeObject(value), timeInSeconds);
        }

        public static T GetT<T>(string key, string databaseIdKey = null)
        {
            var result = GetDatabase(databaseIdKey).StringGet(key);
            if (result.IsNullOrEmpty)
                return default(T);
            var serialized = JsonConvert.DeserializeObject<T>(result);
            return serialized;
        }

        public static async Task<HashEntry[]> GetAllT(string hashKey, string databaseIdKey = null)
        {
            var result = await GetDatabase(databaseIdKey).HashGetAllAsync("Employees");
            return result ?? new HashEntry[0];
        }

        public static bool Delete(string key, string databaseIdKey = null)
        {
            return GetDatabase(databaseIdKey).KeyDelete(key);
        }

        public static T Refresh<T>(string key, object value, string databaseIdKey = null)
        {
            var database = GetDatabase(databaseIdKey);
            database.KeyDelete(key);
            var timeInSeconds = TimeSpan.FromHours(20);
            database.StringSet(key, JsonConvert.SerializeObject(value), timeInSeconds);
            var result = database.StringGet(key);
            var serialized = JsonConvert.DeserializeObject<T>(result);
            return serialized;
        }

        public static IDatabase GetDatabase(string databaseIdKey = null)
        {
            var databaseId = 0;
            if (!string.IsNullOrEmpty(databaseIdKey))
            {
                databaseId = Convert.ToInt32(Configuration.Value.GetSection("RedisConfig")[databaseIdKey]);
            }

            // GetDatabase(int) is a lightweight handle over the shared multiplexer — safe to resolve per call,
            // and this removes the old cross-thread race on the shared static Database field.
            return Multiplexer.Value.GetDatabase(databaseId);
        }
    }
}
