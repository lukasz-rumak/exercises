using TicTacToe.Interfaces;
using TicTacToe.Managers;
using TicTacToe.Models;
using Xunit;

namespace TicTacToeTests
{
    public class ResultUnitTests
    {
        private readonly IResult _result;
        
        public ResultUnitTests()
        {
            _result = new Result();
        }
        
        [Fact]
        public void UnitTestsForResult1()
        {
            var board = new BuildBoard();
            var actual = _result.CheckForWinner(board.Board);
            Assert.Equal(Player.None, actual);
        }
        
        [Fact]
        public void UnitTestsForResult2()
        {
            var board = new BuildBoard();
            board.Board[0, 0].Player = Player.O;
            board.Board[0, 1].Player = Player.O;
            board.Board[0, 2].Player = Player.O;
            var actual = _result.CheckForWinner(board.Board);
            Assert.Equal(Player.O, actual);
        }
        
        [Fact]
        public void UnitTestsForResult3()
        {
            var board = new BuildBoard();
            board.Board[1, 0].Player = Player.X;
            board.Board[1, 1].Player = Player.X;
            board.Board[1, 2].Player = Player.X;
            var actual = _result.CheckForWinner(board.Board);
            Assert.Equal(Player.X, actual);
        }
        
        [Fact]
        public void UnitTestsForResult4()
        {
            var board = new BuildBoard();
            board.Board[0, 0].Player = Player.O;
            board.Board[1, 1].Player = Player.O;
            board.Board[2, 2].Player = Player.O;
            var actual = _result.CheckForWinner(board.Board);
            Assert.Equal(Player.O, actual);
        }
        
        [Fact]
        public void UnitTestsForResult5()
        {
            var board = new BuildBoard();
            board.Board[2, 0].Player = Player.X;
            board.Board[1, 1].Player = Player.X;
            board.Board[0, 2].Player = Player.X;
            var actual = _result.CheckForWinner(board.Board);
            Assert.Equal(Player.X, actual);
        }
        
        [Fact]
        public void UnitTestsForResult6()
        {
            var board = new BuildBoard();
            board.Board[2, 0].Player = Player.X;
            board.Board[1, 1].Player = Player.O;
            board.Board[0, 2].Player = Player.X;
            var actual = _result.CheckForWinner(board.Board);
            Assert.Equal(Player.None, actual);
        }
        
        [Fact]
        public void UnitTestsForResult7()
        {
            var board = new BuildBoard();
            board.Board[0, 0].Player = Player.X;
            board.Board[0, 1].Player = Player.X;
            board.Board[0, 2].Player = Player.O;
            board.Board[1, 0].Player = Player.O;
            board.Board[1, 1].Player = Player.X;
            board.Board[1, 2].Player = Player.X;
            board.Board[2, 0].Player = Player.X;
            board.Board[2, 1].Player = Player.O;
            board.Board[2, 2].Player = Player.O;
            var actual = _result.CheckForWinner(board.Board);
            Assert.Equal(Player.None, actual);
        }
        
        [Fact]
        public void UnitTestsForResult8()
        {
            var board = new BuildBoard();
            board.Board[0, 0].Player = Player.X;
            board.Board[0, 1].Player = Player.O;
            board.Board[0, 2].Player = Player.X;
            board.Board[1, 0].Player = Player.O;
            board.Board[1, 1].Player = Player.X;
            board.Board[1, 2].Player = Player.O;
            board.Board[2, 0].Player = Player.X;
            board.Board[2, 1].Player = Player.O;
            board.Board[2, 2].Player = Player.X;
            var actual = _result.CheckForWinner(board.Board);
            Assert.Equal(Player.X, actual);
        }
    }
}