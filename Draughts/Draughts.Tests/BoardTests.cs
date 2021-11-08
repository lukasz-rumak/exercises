using System.Linq;
using Draughts.Interfaces;
using Draughts.Managers;
using Draughts.Models;
using Xunit;

namespace Draughts.Tests
{
    public class BoardTests
    {
        private readonly IBoardCreator _board;
            
        public BoardTests()
        {
            _board = new BoardCreator(8, 12);
        }
        
        [Fact]
        public void BoardSizeShouldBeEqualTo64()
        {
            var boardCreated = _board.GetBoard();
            var boardSize = boardCreated.Count;
            Assert.Equal(64, boardSize);
        }
        
        [Fact]
        public void BoardPlayableFieldsShouldBeEqualTo32()
        {
            var boardCreated = _board.GetBoard();
            var boardPlayableFields = boardCreated.Values.Count(f => f.Playable == true);
            Assert.Equal(32, boardPlayableFields);
        }
        
        [Fact]
        public void BoardPlayer1CountShouldBeEqualTo12()
        {
            var boardCreated = _board.GetBoard();
            var boardPlayer1Count = boardCreated.Values.Count(f => f.Player == Players.Player1);
            Assert.Equal(12, boardPlayer1Count);
        }
        
        [Fact]
        public void BoardPlayer2CountShouldBeEqualTo12()
        {
            var boardCreated = _board.GetBoard();
            var boardPlayer2Count = boardCreated.Values.Count(f => f.Player == Players.Player2);
            Assert.Equal(12, boardPlayer2Count);
        }

        [Fact]
        public void BoardField00ShouldBePlayable()
        {
            var boardCreated = _board.GetBoard();
            var result = boardCreated[(0, 0)].Playable;
            Assert.True(result);
        }

        [Fact]
        public void BoardFiled01ShouldBeNotPlayable()
        {
            var boardCreated = _board.GetBoard();
            var result = boardCreated[(0, 1)].Playable;
            Assert.False(result);
        }
    }
}