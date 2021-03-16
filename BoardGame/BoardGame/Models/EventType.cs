namespace BoardGame.Models
{
    public enum EventType
    {
        BoardBuilt,
        PlayerAdded,
        PieceMoved,
        WallCreationDone,
        WallCreationError,
        OutsideBoundaries,
        FieldTaken,
        WallOnTheRoute,
        None
    }
}