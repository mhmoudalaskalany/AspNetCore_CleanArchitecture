using JsonNet.ContractResolvers;
using Newtonsoft.Json;

namespace Template.Common.Extensions
{
    public static class Seeder<T> where T : class
    {
        public static T SeedIt(string jsonData)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };
            return JsonConvert.DeserializeObject<T>(jsonData, settings);
        }
    }
}