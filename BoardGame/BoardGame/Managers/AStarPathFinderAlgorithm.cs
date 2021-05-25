using System;
using System.Collections.Generic;
using System.Linq;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class AStarPathFinderAlgorithm : IAStarPathFinderAlgorithm
    {
        private readonly Dictionary<Piece, Func<Tile, List<Tile>>> _piecePossibleMoves;

        public AStarPathFinderAlgorithm()
        {
            _piecePossibleMoves = CreatePossibleMovesDict();
        }

        public bool IsPathPossibleUsingAStarSearchAlgorithm(Piece pieceType, List<Wall> walls, int boardSize, int piecePositionX, int piecePositionY,
            int berryPositionX, int berryPositionY)
        {
            var start = new Tile
            {
                Y = piecePositionY,
                X = piecePositionX
            };

            var finish = new Tile
            {
                Y = berryPositionY,
                X = berryPositionX
            };

            start.SetDistance(finish.X, finish.Y);

            var activeTiles = new List<Tile>
            {
                start
            };
            var visitedTiles = new List<Tile>();

            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                    return true;

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);

                var walkableTiles = GetWalkableTiles(pieceType, walls, boardSize, checkTile, finish);

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

        private List<Tile> GetWalkableTiles(Piece pieceType, List<Wall> walls, int boardSize, Tile currentTile, Tile targetTile)
        {
            var possibleTiles = _piecePossibleMoves[pieceType](currentTile);
            
            possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

            var maxX = boardSize - 1;
            var maxY = boardSize - 1;

            return possibleTiles
                .Where(tile => tile.X >= 0 && tile.X <= maxX)
                .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
                .Where(tile => IsMovePossible(walls, currentTile.X, currentTile.Y, tile.X, tile.Y))
                .ToList();
        }
        
        private bool IsMovePossible(IReadOnlyCollection<Wall> walls, int currentX, int currentY, int newX, int newY)
        {
            if (walls == null) return true;
            if (!walls.Any(wall =>
                wall.WallPositionField1.Item1 == currentX &&
                wall.WallPositionField1.Item2 == currentY &&
                wall.WallPositionField2.Item1 == newX && wall.WallPositionField2.Item2 == newY))
                return true;
            return false;
        }

        private Dictionary<Piece, Func<Tile, List<Tile>>> CreatePossibleMovesDict()
        {
            return new Dictionary<Piece, Func<Tile, List<Tile>>>
            {
                [Piece.Pawn] = GetPossibleTilesForPawn,
                [Piece.Knight] = GetPossibleTilesForKnight,
            };
        }
        
        private List<Tile> GetPossibleTilesForPawn(Tile currentTile)
        {
            return new List<Tile>
            {
                new Tile {X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new Tile {X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new Tile {X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1},
                new Tile {X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1}
            };
        }

        private List<Tile> GetPossibleTilesForKnight(Tile currentTile)
        {
            return new List<Tile>
            {
                new Tile
                    {X = currentTile.X + 1, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new Tile
                    {X = currentTile.X + 1, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new Tile
                    {X = currentTile.X - 1, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new Tile
                    {X = currentTile.X - 1, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1}
            };
        }
    }
}