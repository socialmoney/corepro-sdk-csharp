using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK.Utils
{
    public class JsonSerializer
    {
        private readonly JsonSerializerSettings _settings;
        private readonly Newtonsoft.Json.JsonSerializer _serializer;

        public JsonSerializer()
            : this(new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new JsonContractResolver()
            })
        {
        }

        public JsonSerializer(JsonSerializerSettings settings)
        {
            _settings = settings;
            _settings.Converters.Add(new IsoDateTimeConverter());
            _settings.Converters.Add(new StringEnumConverter());
            _settings.Formatting = Formatting.Indented;
            _serializer = Newtonsoft.Json.JsonSerializer.Create(_settings);
        }

        #region Implementation of ISerializer<string>

        public string Serialize(object obj)
        {
            var mem = new MemoryStream();
            using (var jsonTextWriter = new JsonTextWriter(new StreamWriter(mem, new UTF8Encoding(false, true))) { CloseOutput = false })
            {
                _serializer.Serialize(jsonTextWriter, obj);
                jsonTextWriter.Flush();
            }

            mem.Position = 0;

            return new StreamReader(mem).ReadToEnd();
        }

        public object Deserialize(Type objectType, string obj)
        {
            using (var streamReader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(obj)), new UTF8Encoding(false, true)))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                return _serializer.Deserialize(jsonTextReader, objectType);
            }
        }

        public object Deserialize(string obj)
        {
            using (var streamReader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(obj)), new UTF8Encoding(false, true)))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                return _serializer.Deserialize(jsonTextReader, typeof(object));
            }
        }

        public T Deserialize<T>(string obj)
        {
            using (var streamReader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(obj)), new UTF8Encoding(false, true)))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                return _serializer.Deserialize<T>(jsonTextReader);
            }
        }

        #endregion
    }
}
