using System;
using System.Collections.Generic;
using System.Linq;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class AStarPathFinderAlgorithm : IAStarPathFinderAlgorithm
    {
        public bool DoesPathExist(List<(int, int)> movementRules, List<Obstacle> obstacles, 
            int boardSize, int startX, int startY, int targetX, int targetY)
        {
            var start = CreateTile(startX, startY);
            var finish = CreateTile(targetX, targetY);
            start.SetDistance(finish.X, finish.Y);
            var activeTiles = new List<Tile> { start };
            var visitedTiles = new List<Tile>();

            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                    return true;

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);

                var walkableTiles = GetWalkableTiles(movementRules, obstacles, boardSize, checkTile, finish);

                foreach (var walkableTile in walkableTiles)
                {
                    if (visitedTiles.Any(tile => tile.HasSamePosition(walkableTile)))
                        continue;

                    if (activeTiles.Any(tile => tile.HasSamePosition(walkableTile)))
                    {
                        var existingTile = activeTiles.Single(tile => tile.HasSamePosition(walkableTile));
                        if (existingTile.CostDistance > checkTile.CostDistance)
                        {
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(walkableTile);
                        }
                    }
                    else
                    {
                        activeTiles.Add(walkableTile);
                    }
                }
            }

            return false;
        }

        private Tile CreateTile(int x, int y)
        {
            return new Tile
            {
                Y = y,
                X = x
            };
        }

        private List<Tile> GetWalkableTiles(List<(int, int)> possibleMoves, List<Obstacle> obstacles, int boardSize, Tile currentTile, Tile targetTile)
        {
            var possibleTiles = GetPossibleTiles(possibleMoves, currentTile);
            
            possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

            var maxX = boardSize - 1;
            var maxY = boardSize - 1;

            return possibleTiles
                .Where(tile => tile.X >= 0 && tile.X <= maxX)
                .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
                .Where(tile => IsMovePossible(obstacles, currentTile.X, currentTile.Y, tile.X, tile.Y))
                .ToList();
        }

        private bool IsMovePossible(List<Obstacle> obstacles, int currentX, int currentY, int newX, int newY)
        {
            if (obstacles == null) return true;
            foreach (var obstacle in obstacles)
            {
                if (obstacle.FromX == null || obstacle.FromY == null)
                {
                    if (obstacle.ToX == newX && obstacle.ToY == newY)
                        return false;
                }
                else
                {
                    if (obstacle.FromX == currentX &&
                        obstacle.FromY == currentY &&
                        obstacle.ToX == newX &&
                        obstacle.ToY == newY)
                        return false;
                }
            }

            return true;
        }

        private List<Tile> GetPossibleTiles(List<(int, int)> possibleMoves, Tile currentTile)
        {
            return possibleMoves.Select(item => new Tile
            {
                X = currentTile.X + item.Item1, Y = currentTile.Y + item.Item2, Parent = currentTile,
                Cost = currentTile.Cost + 1
            }).ToList();
        }
    }
}