namespace BoardGame.Interfaces
{
    public interface IBoardBuilder
    {
        IBoardBuilder WithSize(int size);
        IBoardBuilder GenerateBoard();
        IGameBoard Build();
    }
}