using BoardGame.Interfaces;

namespace BoardGame.Models.Berries
{
    public class ErrorBerry : IBerry
    {
        public (int, int) BerryPosition { get; set; }
        public bool IsEaten { get; set; }
    }
}