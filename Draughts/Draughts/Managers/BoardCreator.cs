using System.Collections.Generic;
using System.Linq;
using Draughts.Interfaces;
using Draughts.Models;

namespace Draughts.Managers
{
    public class BoardCreator : IBoardCreator
    {
        private readonly Dictionary<(int, int), Field> _board;
        private readonly int _boardSize;
        
        public BoardCreator(int boardSize, int pawnsNumberPerPlayer)
        {
            _boardSize = boardSize;
            _board = CreateBoard(boardSize, pawnsNumberPerPlayer);
        }
        
        public Dictionary<(int, int), Field> GetBoard()
        {
            return _board;
        }

        public int GetBoardSize()
        {
            return _boardSize;
        }

        public int ReturnNumberOfPawnOnTheBoard()
        {
            return _board.Count(x => x.Value.Player != Players.None);
        }

        public Players ReturnPlayerNameFromTheField((int x, int y) position)
        {
            return position.x >= 0 && position.x < _boardSize && position.y >= 0 && position.y < _boardSize 
                ? _board[position].Player : Players.None;
        }

        private Dictionary<(int, int), Field> CreateBoard(int boardSize, int pawnsNumberPerPlayer)
        {
            var board = new Dictionary<(int, int), Field>();
            
            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    var isPlayable = CheckIfPawnShouldBeAdded(x, y);
                    board[(x, y)] = new Field
                    {
                        Playable = isPlayable,
                        Player = Players.None
                    };
                }
            }
            
            AddPlayerPawnsToBoard(board, Players.Player1, boardSize, pawnsNumberPerPlayer);
            AddPlayerPawnsToBoard(board, Players.Player2, boardSize, pawnsNumberPerPlayer);

            return board;
        }

        private void AddPlayerPawnsToBoard(Dictionary<(int, int), Field> board, Players player, int boardSize,
            int pawnNumberToAdd)
        {
            if (player == Players.Player1)
                for (int y = 0; y < boardSize; y++)
                for (int x = 0; x < boardSize; x++)
                {
                    if (pawnNumberToAdd == 0) return;
                    pawnNumberToAdd = AddPawnToBoard(board, player, x, y, pawnNumberToAdd);
                }
            else if (player == Players.Player2)
                for (int y = boardSize - 1; y >= 0; y--)
                for (int x = boardSize - 1; x >= 0; x--)
                {
                    if (pawnNumberToAdd == 0) return;
                    pawnNumberToAdd = AddPawnToBoard(board, player, x, y, pawnNumberToAdd);
                }
        }

        private int AddPawnToBoard(Dictionary<(int, int), Field> board, Players player, int x, int y, int pawnNumberToAdd)
        {
            if (!CheckIfPawnShouldBeAdded(x, y)) return pawnNumberToAdd;
            board[(x, y)].Player = player;
            return pawnNumberToAdd - 1;
        }
        
        private bool CheckIfPawnShouldBeAdded(int x, int y)
        {
            return y % 2 == x % 2;
        }
    }
}