using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IBoardBuilder
    {
        Field[,] Board { get; }
        int WithSize { get; }
    }
}