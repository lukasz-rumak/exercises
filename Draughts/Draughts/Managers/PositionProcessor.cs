using System;
using System.Collections.Generic;
using Draughts.Interfaces;
using Draughts.Models;

namespace Draughts.Managers
{
    public class PositionProcessor : IPositionProcessor
    {
        private readonly Dictionary<Players, Dictionary<Direction, Func<(int, int)>>> _dictionaryWithPossibilitiesOfMovement;

        public PositionProcessor()
        {
            _dictionaryWithPossibilitiesOfMovement = CreateDictionaryWithPossibilitiesOfMovement();
        }
        
        public Positions ReturnPositions(Players player, Direction direction, int currentX, int currentY)
        {
            return CalculatePositions(currentX, currentY, _dictionaryWithPossibilitiesOfMovement[player][direction].Invoke());
        }
        
        private Positions CalculatePositions(int x, int y, (int x, int y) incrementation)
        {
            return new Positions
            {
                NearbyX = x + incrementation.x,
                NearbyY = y + incrementation.y,
                DistantX = x + incrementation.x + incrementation.x,
                DistantY = y + incrementation.y + incrementation.y
            };
        }

        private Dictionary<Players, Dictionary<Direction, Func<(int, int)>>> CreateDictionaryWithPossibilitiesOfMovement()
        {
            return new Dictionary<Players, Dictionary<Direction, Func<(int, int)>>>
            {
                [Players.Player1] = new()
                {
                    [Direction.Left] = Player1Left,
                    [Direction.Right] = Player1Right
                },
                [Players.Player2] = new()
                {
                    [Direction.Left] = Player2Left,
                    [Direction.Right] = Player2Right
                }
            };
        }

        private (int, int) Player1Left()
        {
            return (-1, 1);
        }

        private (int, int) Player1Right()
        {
            return (1, 1);
        }

        private (int, int) Player2Left()
        {
            return (-1, -1);
        }

        private (int, int) Player2Right()
        {
            return (1, -1);
        }
    }
}