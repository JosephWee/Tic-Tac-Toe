using Newtonsoft.Json.Linq;
using TicTacToe.BusinessLogic;
using TicTacToe.Entity;
using TicTacToe.ML;
using static TicTacToe.BusinessLogic.ComputerPlayerV3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add connection strings
builder.Configuration.AddJsonFile("connectionStrings.json",
        optional: false,
        reloadOnChange: true);

var app = builder.Build();

Random random = new Random();

TicTacToe.BusinessLogic.ComputerPlayerConfig
    .RegisterComputerPlayer<ComputerPlayerV3>(
        () =>
        {
            var computerPlayer = new ComputerPlayerV3();
            computerPlayer.GetMoveHandler += (e) =>
            {
                Dictionary<float, List<int>> winningMoves = new Dictionary<float, List<int>>();
                Dictionary<float, List<int>> blockingMoves = new Dictionary<float, List<int>>();
                
                for (int i = 0; i < e.BlankCells.Count; i++)
                {
                    var blankCell = e.BlankCells.ElementAt<TicTacToeDataEntry>(i);
                    int ci = blankCell.CellIndex;
                    var inputModel1 =
                        new MLModel1.ModelInput()
                        {
                            MoveNumber = e.Cells.Count - e.BlankCells.Count + 1,
                            Cell0 = ci == 0 ? 2 : e.Cells.ElementAt<ReadonlyCell>(0).CellState.CellContent,
                            Cell1 = ci == 1 ? 2 : e.Cells.ElementAt<ReadonlyCell>(1).CellState.CellContent,
                            Cell2 = ci == 2 ? 2 : e.Cells.ElementAt<ReadonlyCell>(2).CellState.CellContent,
                            Cell3 = ci == 3 ? 2 : e.Cells.ElementAt<ReadonlyCell>(3).CellState.CellContent,
                            Cell4 = ci == 4 ? 2 : e.Cells.ElementAt<ReadonlyCell>(4).CellState.CellContent,
                            Cell5 = ci == 5 ? 2 : e.Cells.ElementAt<ReadonlyCell>(5).CellState.CellContent,
                            Cell6 = ci == 6 ? 2 : e.Cells.ElementAt<ReadonlyCell>(6).CellState.CellContent,
                            Cell7 = ci == 7 ? 2 : e.Cells.ElementAt<ReadonlyCell>(7).CellState.CellContent,
                            Cell8 = ci == 8 ? 2 : e.Cells.ElementAt<ReadonlyCell>(8).CellState.CellContent,
                            GameResultCode = 0
                        };

                    var prediction1 = MLModel1.Predict(inputModel1);

                    var PredictionLabel = prediction1.PredictedLabel;
                    var PredictionScore = prediction1.Score;

                    if (PredictionLabel == 1)
                    {
                        if (blockingMoves.ContainsKey(PredictionScore[0]))
                            blockingMoves[PredictionScore[0]].Add(ci);
                        else
                        {
                            blockingMoves.Add(
                                PredictionScore[0],
                                new List<int>() { ci }
                            );
                        }
                    }
                    else if (PredictionLabel == 2)
                    {
                        if (winningMoves.ContainsKey(PredictionScore[0]))
                            winningMoves[PredictionScore[0]].Add(ci);
                        else
                        {
                            winningMoves.Add(
                                PredictionScore[0],
                                new List<int>() { ci }
                            );
                        }
                    }
                }

                int cellIndex = int.MinValue;

                if (winningMoves.Keys.Count > 0)
                {
                    var key = winningMoves.Keys.Max();
                    cellIndex = winningMoves[key].FirstOrDefault();
                }
                else if (blockingMoves.Keys.Count > 0)
                {
                    var key = blockingMoves.Keys.Max();
                    cellIndex = blockingMoves[key].FirstOrDefault();
                }
                else
                {
                    int m = random.Next(0, e.BlankCells.Count - 1);
                    cellIndex = e.BlankCells.ElementAt<TicTacToeDataEntry>(m).CellIndex;
                }

                return cellIndex;
            };

            return computerPlayer;
        }
    );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
