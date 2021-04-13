namespace BoardGame.Models
{
    public enum EventType
    {
        GameStarted,
        BoardBuilt,
        PlayerAdded,
        PieceMoved,
        WallCreationDone,
        WallCreationError,
        BerryCreationDone,
        BerryCreationError,
        BerryEaten,
        OutsideBoundaries,
        FieldTaken,
        WallOnTheRoute,
        IncorrectPlayerId,
        GeneratedBoardOutput,
        None
    }
}