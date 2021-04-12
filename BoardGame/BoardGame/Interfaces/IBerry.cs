namespace BoardGame.Interfaces
{
    public interface IBerry
    {
        public (int, int) BerryPosition { get; set; }
        public bool IsEaten { get; set; }
    }
}