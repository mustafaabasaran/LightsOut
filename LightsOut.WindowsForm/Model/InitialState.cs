using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LightsOut.WindowsForm.Model
{
    public class InitialState
    {
        [JsonPropertyName("row")]
        public int Row { get; set; }
        
        [JsonPropertyName("column")]
        public int Column { get; set; }

        [JsonPropertyName("state")]
        public byte State { get; set; }
    }
}
