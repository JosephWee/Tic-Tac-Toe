using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.BusinessLogic
{
    public class ComputerPlayerBase : ITicTacToeComputerPlayer
    {
        protected static Random random = new Random();

        protected int _PlayerSymbolOpponent = 1;
        public int PlayerSymbolOpponent
        {
            get
            {
                return _PlayerSymbolOpponent;
            }
        }
        
        protected int _PlayerSymbolSelf = 1;
        public int PlayerSymbolSelf
        {
            get
            {
                return _PlayerSymbolSelf;
            }
        }

        public void SetPlayerSymbols(int playerSymbolOpponent, int playerSymbolSelf)
        {
            if (playerSymbolOpponent == playerSymbolSelf)
                throw new ArgumentException("Symbols used for Player 1 and Player 2 must be different.");
            _PlayerSymbolOpponent = playerSymbolOpponent;
            _PlayerSymbolSelf = playerSymbolSelf;
        }

        public ComputerPlayerBase(int playerSymbolOpponent, int playerSymbolSelf)
        {
            SetPlayerSymbols(playerSymbolOpponent, playerSymbolSelf);
        }

        public virtual int GetMove(string InstanceId)
        {
            var ds = TicTacToe.GetAndValidatePreviousMove(InstanceId);

            if (!ds.Any())
                return -1;

            int GridSize = ds.First().GridSize;
            List<int> CellStates = ds.Select(x => x.CellContent).ToList();
            int BlankCellCount = int.MinValue;
            List<int> WinningCells = null;

            var GameStatus = TicTacToe.EvaluateResult(this, GridSize, CellStates, out BlankCellCount, out WinningCells);

            if (GameStatus == Models.TicTacToeGameStatus.InProgress && BlankCellCount > 0)
            {
                int i = random.Next(0, BlankCellCount - 1);
                var blankCells = ds.Where(x => x.CellContent == 0).ToList();
                return blankCells[i].CellIndex;
            }

            return -1;
        }

        public int GetRandomMove(List<int> CellStates)
        {
            List<int> validMoves = new List<int>();
            for (int c = 0; c < CellStates.Count; c++)
            {
                if (CellStates[c] == 0)
                    validMoves.Add(c);
            }

            int index = random.Next(validMoves.Count);
            return validMoves[index];
        }
    }
}