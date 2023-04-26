using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicTacToe.Entity;

namespace TicTacToe.BusinessLogic
{
    public class ComputerPlayerBase : ITicTacToeComputerPlayer
    {
        public class Cell
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public int CellState { get; set; }
        }

        public class CellCollction
        {
            public List<Cell> Cells { get; set; }
            public int OpponentCount { get; set; }
            public int SelfCount { get; set; }
        }

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

        public virtual int GetMove(int GridSize, List<int> CellStates)
        {
            return GetRandomMove(CellStates);
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