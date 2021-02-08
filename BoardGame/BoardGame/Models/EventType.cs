namespace BoardGame.Models
{
    public enum EventType
    {
        PieceMove,
        WallCreationError,
        OutsideBoundaries,
        FieldTaken,
        WallOnTheRoute,
        None
    }
}