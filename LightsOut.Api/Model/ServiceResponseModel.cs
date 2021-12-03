using System;
using System.Text.Json.Serialization;

namespace LightsOut.Api.Model
{
    [Serializable]
    public class ServiceResponseModel<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
        
        [JsonPropertyName("header")]
        public ServiceResponseHeader Header { get; set; }
    }
}