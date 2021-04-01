namespace BoardGameApiTests.Models
{
    public class BadRequestWithSize
    {
        public string[] WithSize { get; set; }
    }

    public class BadRequestPlayerId
    {
        public string[] PlayerId { get; set; }
    }

    public class BadRequestMoveTo
    {
        public string[] MoveTo { get; set; }
    }

    public class BadRequestPlayerType
    {
        public string[] PlayerType { get; set; }
    }

    public class BadRequestWallCoordinates
    {
        public string[] WallCoordinates { get; set; }
    }
}