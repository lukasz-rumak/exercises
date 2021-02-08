namespace BoardGame.Interfaces
{
    public interface IBoardBuilder
    {
        IBoardBuilder WithSize(int size);
        IBoardBuilder AddWall(string instruction);
        IGameBoard BuildBoard();
    }
}