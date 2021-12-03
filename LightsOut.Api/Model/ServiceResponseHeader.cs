using System.Text.Json.Serialization;

namespace LightsOut.Api.Model
{
    public class ServiceResponseHeader
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = "SUCCESS";
        
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; } = 200;
    }
}