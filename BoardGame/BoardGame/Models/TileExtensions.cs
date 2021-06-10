namespace BoardGame.Models
{
    public static class TileExtensions
    {
        public static bool HasSamePosition(this Tile tile, Tile other)
        {
            return tile.X == other.X && tile.Y == other.Y;
        }
    }
}