namespace Draughts.Models
{
    public class Event
    {
        public (int, int) Source { get; set; }
        public (int, int) Destination { get; set; }
        public Action Action { get; set; }
    }
}