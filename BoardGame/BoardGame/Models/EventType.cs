namespace BoardGame.Models
{
    public enum EventType
    {
        GameStarted,
        BoardCreationDone,
        BoardCreationError,
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
        IncorrectBoardSize,
        None
    }
}