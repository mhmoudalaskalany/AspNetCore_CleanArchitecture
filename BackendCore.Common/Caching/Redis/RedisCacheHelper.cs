using System;
using System.Threading.Tasks;
using BackendCore.Common.Configurations;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BackendCore.Common.Caching.Redis
{
    public static class RedisCacheHelper
    {
        public static IDatabase Database { get; set; }
        public static bool Set(string key, object value)
        {
            if (Database == null)
            {
                Database = GetDatabase();
            }
            Database.StringSet(key, JsonConvert.SerializeObject(value));
            return true;
        }

        public static void Set(string key, object value, int time)
        {

            if (Database == null)
            {
                Database = GetDatabase();
            }
            var timeInSeconds = TimeSpan.FromHours(time);
            Database.StringSet(key, JsonConvert.SerializeObject(value), timeInSeconds);
        }

        public static T GetT<T>(string key)
        {
            if (Database == null)
            {
                Database = GetDatabase();
            }
            var result = Database.StringGet(key);
            if (result.IsNullOrEmpty)
                return default(T);
            var serialized = JsonConvert.DeserializeObject<T>(result);
            return serialized;
        }
        public  static async Task<HashEntry[]> GetAllT(string hashKey)
        {
            if (Database == null)
            {
                Database = GetDatabase();
            }
            var result = await Database.HashGetAllAsync("Employees");
            if (result != null)
            {
                return result;
            }
            return new HashEntry[0];
        }
        public static bool Delete(string key)
        {
            if (Database == null)
            {
                Database = GetDatabase();
            }
            var result = Database.KeyDelete(key);
            return result;
        }
        public static T Refresh<T>(string key, object value)
        {
            if (Database == null)
            {
                Database = GetDatabase();
            }
            Database.KeyDelete(key);
            var timeInSeconds = TimeSpan.FromHours(20);
            Database.StringSet(key, JsonConvert.SerializeObject(value), timeInSeconds);
            var result = Database.StringGet(key);
            var serialized = JsonConvert.DeserializeObject<T>(result);
            return serialized;
        }



        public static IDatabase GetDatabase()
        {
            IConfigurationRoot root = AppSettingsConfigurations.ReadConfigurationFromAppSettings();
            var redisConfig = root.GetSection("RedisConfig");
            var connection = redisConfig["StackExchangeConnection"];
            //IDatabase DatabaseReturn = null;
            string connectionString = connection;
            var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            if (connectionMultiplexer.IsConnected)
                Database = connectionMultiplexer.GetDatabase();

            return Database;
        }
    }
}