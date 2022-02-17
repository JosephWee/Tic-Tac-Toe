using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.BusinessLogic
{
    public class TicTacToe
    {
        public static Models.TicTacToeUpdateResponse EvaluateResult(Models.TicTacToeUpdateRequest request)
        {
            int winner = int.MinValue;
            bool gameOver = true;

            int[,] cell = new int[request.GridSize, request.GridSize];

            for (int i = 0; i < request.TotalCellCount; i++)
            {
                int col = i % request.GridSize;
                int row = (i - col + 1) % request.GridSize;
                int cellState = request.CellStates[i];
                cell[row, col] = cellState;

                if (cellState == 0)
                    gameOver = false;
            }

            //Check every row
            if (!gameOver)
            {
                for (int r = 0; r < request.GridSize; r++)
                {
                    bool allTheSame = true;
                    int prevCellState = cell[r, 0];
                    for (int c = 1; c < request.GridSize; c++)
                    {
                        int cellState = cell[r, c];
                        if (cellState != prevCellState)
                        {
                            allTheSame = false;
                            break;
                        }
                    }

                    if (cell[r, 0] != 0 && allTheSame)
                    {
                        winner = cell[r, 0];
                        gameOver = true;
                    }
                }
            }

            //Check every column
            if (!gameOver)
            {
                for (int c = 0; c < request.GridSize; c++)
                {
                    bool allTheSame = true;
                    int prevCellState = cell[0, c];
                    for (int r = 1; r < request.GridSize; r++)
                    {
                        int cellState = cell[r, c];
                        if (cellState != prevCellState)
                        {
                            allTheSame = false;
                            break;
                        }
                    }

                    if (cell[0, c] != 0 && allTheSame)
                    {
                        winner = cell[0, c];
                        gameOver = true;
                    }
                }
            }

            //Check diagonal top-bottom
            if (!gameOver)
            {
                bool allTheSame = true;
                int prevCellState = cell[0, 0];

                for (int i = 0; i < request.GridSize; i++)
                {
                    int cellState = cell[i, i];
                    if (cellState != prevCellState)
                    {
                        allTheSame = false;
                        break;
                    }

                    if (cell[0, 0] != 0 && allTheSame)
                    {
                        winner = cell[0, 0];
                        gameOver = true;
                    }
                }
            }

            //Check diagonal bottom-top
            if (!gameOver)
            {
                bool allTheSame = true;
                int prevCellState = cell[0, 0];

                for (int i = 0; i < request.GridSize; i++)
                {
                    int r = request.GridSize - 1 - i;
                    int cellState = cell[r, i];
                    if (cellState != prevCellState)
                    {
                        allTheSame = false;
                        break;
                    }

                    if (cell[request.GridSize - 1, 0] != 0 && allTheSame)
                    {
                        winner = cell[request.GridSize - 1, 0];
                        gameOver = true;
                    }
                }
            }

            Models.TicTacToeUpdateResponse response =
                new Models.TicTacToeUpdateResponse();

            if (gameOver)
            {
                if (winner == 1)
                    response.Status = Models.TicTacToeGameStatus.Player1Wins;
                else if (winner == 2)
                    response.Status = Models.TicTacToeGameStatus.Player2Wins;
                else
                    response.Status = Models.TicTacToeGameStatus.Draw;
            }
            else
            {
                response.Status = Models.TicTacToeGameStatus.InProgress;
            }

            return response;
        }
    }
}