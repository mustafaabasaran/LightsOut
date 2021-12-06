using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LightsOut.WindowsForm.Model
{
    public class BoardSetting
    {
        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("onColor")]
        public string OnColor { get; set; }

        [JsonPropertyName("offColor")]
        public string OffColor { get; set; }
    }
}
