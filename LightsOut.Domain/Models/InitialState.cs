#nullable disable

namespace LightsOut.Domain.Models
{
    public partial class InitialState
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public byte State { get; set; }
    }
}
