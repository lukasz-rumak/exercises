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
        OutsideBoundaries,
        FieldTaken,
        WallOnTheRoute,
        IncorrectPlayerId,
        GeneratedBoardOutput,
        None
    }
}