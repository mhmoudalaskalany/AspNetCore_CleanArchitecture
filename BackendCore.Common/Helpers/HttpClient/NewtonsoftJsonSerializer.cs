using System.IO;
using Newtonsoft.Json;
using RestSharp.Serializers;

namespace BackendCore.Common.Helpers.HttpClient
{
    public class NewtonsoftJsonSerializer : ISerializer//, IDeserializer
    {
        private readonly JsonSerializer _serializer;

        public NewtonsoftJsonSerializer(JsonSerializer serializer)
        {
            this._serializer = serializer;
        }

        public string ContentType
        {
            get => "application/json";
// Probably used for Serialization?
            set { }
        }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    _serializer.Serialize(jsonTextWriter, obj);

                    return stringWriter.ToString();
                }
            }
        }

        //public T Deserialize<T>(IRestResponse response)
        //{
        //    var content = response.Content;

        //    using (var stringReader = new StringReader(content))
        //    {
        //        using (var jsonTextReader = new JsonTextReader(stringReader))
        //        {
        //            return serializer.Deserialize<T>(jsonTextReader);
        //        }
        //    }
        //}

        public static NewtonsoftJsonSerializer Default =>
            new NewtonsoftJsonSerializer(new JsonSerializer()
            {
                NullValueHandling = NullValueHandling.Ignore,
            });
    }
}
