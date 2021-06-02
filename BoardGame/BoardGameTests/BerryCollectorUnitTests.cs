using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Managers;
using BoardGame.Models;
using BoardGame.Models.Berries;
using Xunit;

namespace BoardGameTests
{
    public class BerryCollectorUnitTests
    {
        [Theory]
        [InlineData(0, 1, 2)]
        [InlineData(1, 0, 1)]
        [InlineData(2, 2, 6)]
        [InlineData(5, 5, 15)]
        [InlineData(12, 16, 44)]
        [InlineData(16, 12, 40)]
        [InlineData(122, 168, 458)]
        [InlineData(168, 122, 412)]
        public void ReturnExpectedVersusActualForBerryCollectorForPawn(int collectedBlueBerry, int collectedStrawBerry, int expectedResult)
        {
            IPiece pawn = new Pawn(0, new List<(int, int)>());
            for (int i = 0; i < collectedBlueBerry; i++)
                pawn.CollectBerry(new BlueBerry());
            for (int i = 0; i < collectedStrawBerry; i++) 
                pawn.CollectBerry(new StrawBerry());
            var actual = pawn.CalculateScore();
            Assert.Equal(expectedResult, actual);
        }
        
        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(1, 0, 2)]
        [InlineData(2, 2, 6)]
        [InlineData(5, 5, 15)]
        [InlineData(12, 16, 40)]
        [InlineData(16, 12, 44)]
        [InlineData(122, 168, 412)]
        [InlineData(168, 122, 458)]
        public void ReturnExpectedVersusActualForBerryCollectorForKnight(int collectedBlueBerry, int collectedStrawBerry, int expectedResult)
        {
            IPiece knight = new Knight(0, new List<(int, int)>());
            for (int i = 0; i < collectedBlueBerry; i++)
                knight.CollectBerry(new BlueBerry());
            for (int i = 0; i < collectedStrawBerry; i++) 
                knight.CollectBerry(new StrawBerry());
            var actual = knight.CalculateScore();
            Assert.Equal(expectedResult, actual);
        }
    }
}