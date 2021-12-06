using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LightsOut.WindowsForm.Model
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
