using Draughts.Managers;
using Draughts.Models;

namespace Draughts
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: co z sytuacja, ze pionki po dojsciu do konca planszy zmieniaja sie w pionki, ktore poruszaja sie po przekatnej
            // TODO: co z sytuacja, ze pionki moga bic inne pionki nie tylko do przodu, ale tez do tylu
            
            var board = new BoardCreator(8, 12);
            var eventHandler = new EventsHandler(board, new EventsCreator(board, new PositionProcessor()));
            var game = new GameHandler(board, new ConsoleOutput(), new MovementHandler(), eventHandler, new HumanPlayerHandler(board, eventHandler, 3));
            
            game.PlayTheGame();
        }
    }
}
