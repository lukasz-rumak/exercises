namespace BoardGame.Interfaces
{
    public interface IBoardBuilder
    {
        IBoardBuilder WithSize(int size);
        IGameBoard BuildBoard();
    }
}