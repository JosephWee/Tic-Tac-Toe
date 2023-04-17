using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.BusinessLogic
{
    public class ComputerPlayerV1 : ITicTacToeComputerPlayer
    {
        private static Random random = new Random();

        public int GetMove(string InstanceId, int LastMoveNumber)
        {
            var ds = TicTacToe.GetAndValidate(InstanceId, LastMoveNumber);

            int GridSize = ds.First().GridSize;
            List<int> CellStates = ds.Select(x => x.CellContent).ToList();
            int BlankCellCount = int.MinValue;
            List<int> WinningCells = null;

            var GameStatus = TicTacToe.EvaluateResult(GridSize, CellStates, out BlankCellCount, out WinningCells);

            if (GameStatus == Models.TicTacToeGameStatus.InProgress && BlankCellCount > 0)
            {
                int i = random.Next(0, BlankCellCount - 1);
                var blankCells = ds.Where(x => x.CellContent == 0).ToList();
                return blankCells[i].CellIndex;
            }

            return -1;
        }
    }
}