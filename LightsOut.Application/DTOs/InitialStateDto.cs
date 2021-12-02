namespace LightsOut.Application.DTOs
{
    public class InitialStateDto
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public byte State { get; set; }
    }
}