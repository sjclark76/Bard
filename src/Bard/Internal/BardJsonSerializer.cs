using System;
using System.Text.Json;

namespace Bard.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public class BardJsonSerializer
    {
        private readonly JsonSerializerOptions? _deSerializerOptions;
        private readonly JsonSerializerOptions _serializationOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deSerializerOptions"></param>
        /// <param name="serializationOptions"></param>
        public BardJsonSerializer(JsonSerializerOptions? deSerializerOptions = null,
            JsonSerializerOptions? serializationOptions = null)
        {
            _deSerializerOptions = deSerializerOptions ?? new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            _serializationOptions = serializationOptions ?? new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        /// <summary>
        /// Deserialize Json string to an object
        /// </summary>
        /// <param name="json"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _deSerializerOptions) ?? throw new InvalidOperationException($"unable to deserialize string {json}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, _serializationOptions);
        }
    }
}