namespace BoardGame.Models
{
    public enum EventType
    {
        PieceMove,
        WallCreationDone,
        WallCreationError,
        OutsideBoundaries,
        FieldTaken,
        WallOnTheRoute,
        None
    }
}