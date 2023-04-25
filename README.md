# Tick-Tac-Toe
## Background
The Tic-Tac-Toe project was started in 16 Feb 2022 as a way to learn about Machine Learning.

The current implementation has 3 versions of Computer Players.
- ComputerPlayerV1 chooses it's moves randomly.
- ComputerPlayerV2 chooses it's moves programatically.
- ComputerPlayerV3 chooses it's moves based on the probability of winning the game using an AI model.

![Tic-Tac-Toe Project Architectural Overview ](/Documentation/Tic-Tac-Toe-Overview-compact.jpeg)

## Objectives
1. Create a Web App that allows human players to play against a Computer Player.
2. Create a Computer Player that can choose it's moves based on the probability of winning the game using an AI model.
3. Create an architecture that supports Computer versus Computer games.
4. Collect game data using the Computer versus Computer games.
5. Explore the possibility of setting up data pipelines which can later be used to learn about ML Ops.

## Quickstart
### Code Entry Point
Open the `Visual Studio 2022` solution by double clicking on `Tic-Tac-Toe.sln` found at the root directory.

For more information see:
- https://visualstudio.microsoft.com/vs/

### Database
The project is currently implemented to use `SQL Server Express LocalDB`.
The LocalDB files can be found at `\WebApi\LocalDB\`.
As the current LocalDB files are locked to my Account, they need to be replaced with your own LocalDB files.
Once you have created your own LocalDB files, you can create the necessary tables using the SQL script `CreateTicTacToeTable.sql` located at `\TicTacToeBL\DBScripts\`

For more information please see:
- https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16
- https://learn.microsoft.com/en-us/visualstudio/data-tools/create-a-sql-database-by-using-a-designer?view=vs-2022

### Web API
#### connectionStrings.json
Replace the connection string named `TicTacToeDataConnString` with the actual value of your connection string.

For example:
```json
{
  "ConnectionStrings": {
    "TicTacToeDataConnString": "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=$(MSBuildProjectDirectory)\\LocalDB\\TicTacToeData.mdf;Integrated Security=True;Connect Timeout=30"
  }
}
```

Please note that the well-known MSBuild property `$(MSBuildProjectDirectory)` is automatically replaced with the `WebApi` project directory if found in the connection string.

For more information see:
- https://learn.microsoft.com/en-us/ef/core/miscellaneous/connection-strings
- https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0
- https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0#file-configuration-provider
- https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-reserved-and-well-known-properties?view=vs-2022

### BlazorServerApp
#### appsettings.json
Replace the `TicTacToeWebApi` endpoint in the `appsettings.json` file with the actual endpoint of the TicTacToe Web API. The baseaddress can be found in the `launchsettings.json` file found at `\WebApi\Properties`.

### Running the code
To run the code, make sure you start the `WebApi` project before starting the `BlazorServerApp` project.

## Observations
As previously mentioned, the purpose of this project is for learning about ML(Machine Learning).
This section will be dedicated to cataloguing about my learning observations.

### 2023-04-24 12:30pm Mon - ComputerPlayerV3
I have noticed that when I play against ComputerPlayerV3, it always make the same `first move` based purely on the `ML Model prediction`. While this is easily recitified with manually adding an element of randomness in the code, it highlights an interesting issue:
```
If a business decision is made purely based on ML (Machine Learning) Model Prediction,
that decision may become predictable.
```
For example, if a newbie ML learner like myself, were to design an automobile navigational system that recommends a route purely based on parameters such as `Shortest Distance`, `Best Fuel Economy`, `Lowest Toll`, etc. our customers may all be routed to the same location because we have disregarded real-time information like `current traffic conditions` and `the number of customers that were recommended the same route`.
