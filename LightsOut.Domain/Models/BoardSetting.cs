#nullable disable

namespace LightsOut.Domain.Models
{
    public partial class BoardSetting
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public string OnColor { get; set; }
        public string OffColor { get; set; }
    }
}
