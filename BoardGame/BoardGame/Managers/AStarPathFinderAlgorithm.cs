using System;
using System.Collections.Generic;
using System.Linq;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class AStarPathFinderAlgorithm : IAStarPathFinderAlgorithm
    {
        public bool IsPathExists(List<(int, int)> movementRules, List<Obstacle> obstacles, 
            int boardSize, int startX, int startY, int targetX, int targetY)
        {
            var start = GetStartPosition(startX, startY);
            var finish = GetFinishPosition(targetX, targetY);
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
                    if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                        continue;

                    if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    {
                        var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
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

        private Tile GetStartPosition(int startX, int startY)
        {
            return new Tile
            {
                Y = startY,
                X = startX
            };
        }

        private Tile GetFinishPosition(int targetX, int targetY)
        {
            return new Tile
            {
                Y = targetY,
                X = targetX
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
            var list = new List<Tile>();
            foreach (var (x, y) in possibleMoves)
            {
                list.Add(new Tile
                {
                    X = currentTile.X + x, Y = currentTile.Y + y, Parent = currentTile, Cost = currentTile.Cost + 1
                });
            }

            return list;
        }
    }
}