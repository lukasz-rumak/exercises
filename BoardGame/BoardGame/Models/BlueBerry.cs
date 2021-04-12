using BoardGame.Interfaces;

namespace BoardGame.Models
{
    public class BlueBerry : IBerry
    {
        public (int, int) BerryPosition { get; set; }
        public bool IsEaten { get; set; }
    }
}