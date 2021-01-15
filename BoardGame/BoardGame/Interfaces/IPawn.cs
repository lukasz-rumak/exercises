namespace BoardGame.Interfaces
{
    public interface IPlayer
    {
        string ExecuteThePlayerInstruction(IValidator validator, IBoardBuilder board, string input);
    }
}