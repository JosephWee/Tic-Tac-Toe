# Tick-Tac-Toe

[MIT License](https://github.com/JosephWee/Tic-Tac-Toe/blob/main/LICENSE)

## Table of Contents
* [About](https://github.com/JosephWee/Tic-Tac-Toe#about)
* [Objectives](https://github.com/JosephWee/Tic-Tac-Toe#objectives)
* [Quickstart](https://github.com/JosephWee/Tic-Tac-Toe#quickstart)
  * [Code Entry Point](https://github.com/JosephWee/Tic-Tac-Toe#code-entry-point)
  * [Database](https://github.com/JosephWee/Tic-Tac-Toe#database)
  * [Web API](https://github.com/JosephWee/Tic-Tac-Toe#web-api)
  * [BlazorServerApp](https://github.com/JosephWee/Tic-Tac-Toe#blazorserverapp)
  * [Running the code](https://github.com/JosephWee/Tic-Tac-Toe#running-the-code)
* [Notes](https://github.com/JosephWee/Tic-Tac-Toe#notes)
  * [2023-04-24 12:30pm Mon - ComputerPlayerV3](https://github.com/JosephWee/Tic-Tac-Toe#2023-04-24-1230pm-mon---computerplayerv3)
  * [2023-04-28 9:59pm Fri - Tic-Tac-Toe Game Stats Part 1](https://github.com/JosephWee/Tic-Tac-Toe#2023-04-28-959pm-fri---tic-tac-toe-game-stats-part-1)
  * [2023-05-01 9:29am Mon - Tic-Tac-Toe Game Stats Part 2](https://github.com/JosephWee/Tic-Tac-Toe#2023-05-01-929am-mon---tic-tac-toe-game-stats-part-2)
    * [Distribution (%) of Winning Player (Player 1) moves across the 9 cells](https://github.com/JosephWee/Tic-Tac-Toe#distribution--of-winning-player-player-1-moves-across-the-9-cells)
    * [Distribution (%) of Winning Player (Player 2) moves across the 9 cells](https://github.com/JosephWee/Tic-Tac-Toe#distribution--of-winning-player-player-2-moves-across-the-9-cells)
    * [Distribution (%) of Winning Player moves across the 9 cells](https://github.com/JosephWee/Tic-Tac-Toe#distribution--of-winning-player-moves-across-the-9-cells)
  * [2023-05-04 4:02pm - Possibility of using GitHub Actions for ML Learning Pipeline](https://github.com/JosephWee/Tic-Tac-Toe#2023-05-04-402pm---possibility-of-using-github-actions-for-ml-learning-pipeline)
  * [2023-05-10 4:23pm Wed - Tic-Tac-Toe Game Stats Part 3](https://github.com/JosephWee/Tic-Tac-Toe#2023-05-10-423pm-wed---tic-tac-toe-game-stats-part-3)
    * [Distribution (%) of Winning Player (Player 1) moves across the 9 cells](https://github.com/JosephWee/Tic-Tac-Toe#distribution--of-winning-player-player-1-moves-across-the-9-cells-1)
    * [Distribution (%) of Winning Player (Player 2) moves across the 9 cells](https://github.com/JosephWee/Tic-Tac-Toe#distribution--of-winning-player-player-2-moves-across-the-9-cells-1)
    * [Distribution (%) of Winning Player moves across the 9 cells](https://github.com/JosephWee/Tic-Tac-Toe#distribution--of-winning-player-moves-across-the-9-cells-1)
  * [2023-05-11 7:28pm Thu - Tic-Tac-Toe Game Stats Part 4](https://github.com/JosephWee/Tic-Tac-Toe#2023-05-11-728pm-thu---tic-tac-toe-game-stats-part-4)
    * [Distribution (%) of Winning Player (Player 1) moves across the 9 cells](https://github.com/JosephWee/Tic-Tac-Toe#distribution--of-winning-player-player-1-moves-across-the-9-cells-2)
    * [Distribution (%) of Winning Player (Player 2) moves across the 9 cells](https://github.com/JosephWee/Tic-Tac-Toe#distribution--of-winning-player-player-2-moves-across-the-9-cells-2)
    * [Distribution (%) of Winning Player moves across the 9 cells](https://github.com/JosephWee/Tic-Tac-Toe#distribution--of-winning-player-moves-across-the-9-cells-2)


## About
The Tic-Tac-Toe project was started in 16 Feb 2022 as a way to learn about Machine Learning. Since then I have added PostgreSQL Database support for running Unit Tests using GitHub Actions and Docker support for better deployment management.

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
#### SQL Server Express LocalDB (Depreciated)
The project was implemented to use `SQL Server Express LocalDB`, but this has been depreciated in favor of PostgreSQL.

The LocalDB file has been removed from `\WebApi\LocalDB\`.

The old DB scripts for SQL Server can still be found at `\TicTacToeBL\DBScripts\` but they are no longer updated.

For more information please see:
- https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16
- https://learn.microsoft.com/en-us/visualstudio/data-tools/create-a-sql-database-by-using-a-designer?view=vs-2022

#### Postgresql
You can now use `PostgreSQL` as the database.

To create the database and login credentials run the DB script `\TicTacToeBL\DBScripts\CreateTicTacToeSchema-Postgresql.sql`.

For more information please see:
- https://www.postgresql.org/docs/current/tutorial-install.html
- https://www.postgresql.org/docs/current/app-psql.html
- https://www.pgadmin.org/docs/pgadmin4/latest/query_tool.html

### Web API
#### Connection Strings

The `\WebAPI\connectionStrings.json` file has been removed.

To make it easier to switch between the different Visual Studios Debugging Profiles the connection strings are now stored as an Environmental Variable `TicTacToeDataConnString` in the `\WebApi\Properties\launchsettings.json`.

For more information see:
- https://learn.microsoft.com/en-us/ef/core/miscellaneous/connection-strings
- https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0
- https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0#file-configuration-provider
- https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-reserved-and-well-known-properties?view=vs-2022

### BlazorServerApp
#### TicTacToe WebApi Endpoint

To make it easier to switch between the different Visual Studios Debugging Profiles the TicTacToe WebApi Endpoints are now stored as an Environmental Variable `TicTacToeWebApiEndPoint` in the `\BlazorServerApp\Properties\launchsettings.json`.

### Running the code
#### Running the code with Docker

Docker Compose is the recommended way to Run/Debug the project as Docker-Compose will start the WebAPI and BlazorServerApp services in the right order.

To run the code with Docker-Compose you need the following installed:
- Visual Studio Community 2022 or Visual Studio Professional 2022
- Docker Desktop
- Docker Compose
- PostgreSQL
- pgAdmin (Optional)

Once you have cloned the code into your local development machine, open it with Visual Studio 2022.

Right-Click on the `docker-compose` solution folder and select `Set as Startup Project` from the context menu.

Then click on the green play button that says `Docker Compose` near the top.

To run the code using other Debugging Profiles, make sure you start the `WebApi` project before starting the `BlazorServerApp` project.

## Notes
As previously mentioned, the purpose of this project is for learning about ML(Machine Learning).
This section will be dedicated to cataloguing about my learning observations.

### 2023-04-24 12:30pm Mon - ComputerPlayerV3
I have noticed that when I play against ComputerPlayerV3, it always make the same `first move` based purely on the `ML Model prediction`. While this is easily recitified with manually adding an element of randomness in the code, it highlights an interesting issue:
```
If a business decision is made purely based on ML (Machine Learning) Model Prediction,
that decision may become predictable.
```
For example, if a newbie ML learner like myself, were to design an automobile navigational system that recommends a route purely based on parameters such as `Shortest Distance`, `Best Fuel Economy`, `Lowest Toll`, etc. our customers may all be routed to the same location because we have disregarded real-time information like `current traffic conditions` and `the number of customers that were recommended the same route`.

### 2023-04-28 9:59pm Fri - Tic-Tac-Toe Game Stats Part 1
In this first entry about the Tic-Tac-Toe game data, I wanted to compare the number of Player 1 Wins, Draws and Player 2 Wins for game setup variations.

The game variations are as follows:
1. Player 1: ComputerPlayerV1  versus  Player 2: ComputerPlayerV1
2. Player 1: ComputerPlayerV2  versus  Player 2: ComputerPlayerV1
3. Player 1: ComputerPlayerV1  versus  Player 2: ComputerPlayerV2
4. Player 1: ComputerPlayerV2  versus  Player 2: ComputerPlayerV2
5. Player 1: ComputerPlayerV2  versus  Player 2: ComputerPlayerV3
6. Player 1: ComputerPlayerV3  versus  Player 2: ComputerPlayerV2
7. Player 1: ComputerPlayerV3  versus  Player 2: ComputerPlayerV3

Player 1 plays `X` while Player 2 plays `O`.
Player 1 always move first.


To recap the difference between ComputerPlayerV1, ComputerPlayerV2 and ComputerPlayerV3:
- ComputerPlayerV1 chooses it's moves randomly.
- ComputerPlayerV2 chooses it's moves programatically.
- ComputerPlayerV3 chooses it's moves based on the probability of winning the game using an AI model.

**Game Setup 1:** ComputerPlayerV1 [X] vs ComputerPlayerV1 [O]

![Tic-Tac-Toe Game Outcome - 2P ComputerPlayerV1 vs ComputerPlayerV1](/Jupyter-Notebook/GameStats/Output/20230429/TicTacToeGameOutcomeByGameSetup2P_Valid_Test_-_P1__ComputerPlayerV1_P2__ComputerPlayerV1.png)

The number of Player 1 wins are more than 2 times the number of Player 2 wins and 5 to 6 times that of Draws. Since both Player 1 and Player 2 chooses their moves randomly, it would suggest that moving first has a significant advantage.

**Game Setup 2:** ComputerPlayerV1 [X] vs ComputerPlayerV2 [O]

![Tic-Tac-Toe Game Outcome - 2P ComputerPlayerV1 vs ComputerPlayerV2](/Jupyter-Notebook/GameStats/Output/20230429/TicTacToeGameOutcomeByGameSetup2P_Valid_Test_-_P1__ComputerPlayerV1_P2__ComputerPlayerV2.png)

The number of Player 2 wins are at least 8 times the number of Player 1 wins and the number of Draws is at least 2 times than the number of Player 1 wins. Since Player 1 chooses it's moves randomly and Player 2 chooses it's moves strategically via an algorithm, it would suggest that choosing one's move strategically has a far greater advantage than moving first.

**Game Setup 3:** ComputerPlayerV2 [X] vs ComputerPlayerV1 [O]

![Tic-Tac-Toe Game Outcome - 2P ComputerPlayerV2 vs ComputerPlayerV1](/Jupyter-Notebook/GameStats/Output/20230429/TicTacToeGameOutcomeByGameSetup2P_Valid_Test_-_P1__ComputerPlayerV2_P2__ComputerPlayerV1.png)

The number of Player 1 wins is more than 9 times the number of Player 2 wins and Draws. This suggests that choosing one's move strategically and moving first has a very significant advantage.

**Game Setup 4:** ComputerPlayerV2 [X] vs ComputerPlayerV2 [O]

![Tic-Tac-Toe Game Outcome - 2P ComputerPlayerV2 vs ComputerPlayerV2](/Jupyter-Notebook/GameStats/Output/20230429/TicTacToeGameOutcomeByGameSetup2P_Valid_Test_-_P1__ComputerPlayerV2_P2__ComputerPlayerV2.png)

There are no Player 1 wins and the number of Player 2 wins are slightly higher than the number of Draws. This suggests that when both players are moving strategically, moving second has a significant advantage.

**Game Setup 5:** ComputerPlayerV2 [X] vs ComputerPlayerV3 [O]

![Tic-Tac-Toe Game Outcome - 2P ComputerPlayerV2 vs ComputerPlayerV3](/Jupyter-Notebook/GameStats/Output/20230429/TicTacToeGameOutcomeByGameSetup2P_Valid_Test_-_P1__ComputerPlayerV2_P2__ComputerPlayerV3.png)

In setup 5, there are no Player 1 or Player 2 wins. It suggests that when both Players move strategically but one Player (P2) has experience (I think of using an AI model which is derived from past data as tapping on one's experience to anticipate the outcome), it results in a no win situation.

**Game Setup 6:** ComputerPlayerV3 [X] vs ComputerPlayerV2 [O]

![Tic-Tac-Toe Game Outcome - 2P ComputerPlayerV3 vs ComputerPlayerV2](/Jupyter-Notebook/GameStats/Output/20230429/TicTacToeGameOutcomeByGameSetup2P_Valid_Test_-_P1__ComputerPlayerV3_P2__ComputerPlayerV2.png)

In setup 6, there are again no Player 1 or Player 2 wins. It suggests that when both Players move strategically but one Player (P1) has experience (I think of using an AI model which is derived from past data as tapping on one's experience to anticipate the outcome), it results in a no win situation.

**Game Setup 7:** ComputerPlayerV3 [X] vs ComputerPlayerV3 [O]

![Tic-Tac-Toe Game Outcome - 2P ComputerPlayerV3 vs ComputerPlayerV3](/Jupyter-Notebook/GameStats/Output/20230429/TicTacToeGameOutcomeByGameSetup2P_Valid_Test_-_P1__ComputerPlayerV3_P2__ComputerPlayerV3.png)

In setup 6, there are again no Player 1 or Player 2 wins. It suggests that when both Players move strategically both Players are experienced players (I think of using an AI model which is derived from past data as tapping on one's experience to anticipate the outcome), it results in a no win situation.

### 2023-05-01 9:29am Mon - Tic-Tac-Toe Game Stats Part 2
In this entry, I am posting 2 heatmaps showing the distribution of the winning player's moves across the 9 cells.

#### Distribution (%) of Winning Player (Player 1) moves across the 9 cells
![Distribution % of Player 1 moves across the 9 cells](/Jupyter-Notebook/GameStats/Output/20230429/P1_Wins_Cells_occupancy_by_percentage.png)

#### Distribution (%) of Winning Player (Player 2) moves across the 9 cells
![Distribution % of Player 2 moves across the 9 cells](/Jupyter-Notebook/GameStats/Output/20230429/P2_Wins_Cells_occupancy_by_percentage.png)

#### Distribution (%) of Winning Player moves across the 9 cells
![Distribution % of Winning Player moves across the 9 cells](/Jupyter-Notebook/GameStats/Output/20230429/Winner_Cells_occupancy_by_percentage.png)

### 2023-05-04 4:02pm - Possibility of using GitHub Actions for ML Learning Pipeline
When working with the `GitHub Actions Workflow` for building and testing my code commits, I noticed that it may be possible to use `GitHub Actions Jobs` with `dotnet test CLI` to create a `ML training pipeline`. It may be a possibility worth exploring for a `Public repository` such as this one, as `GitHub Actions` are `free` for `Public` repositories. If this were to be done, on a `Private repository`, a more careful study of the `costs` involved would need to be carried out.

### 2023-05-10 4:23pm Wed - Tic-Tac-Toe Game Stats Part 3
In this third entry about the Tic-Tac-Toe game data, I wanted to compare the number of Player 1 Wins, Draws and Player 2 Wins when both Player 1 and Player 2 are picking their moves `randomly`.

Player 1 plays `X` while Player 2 plays `O`.
Player 1 always move first.

To recap the difference between ComputerPlayerV1, ComputerPlayerV2 and ComputerPlayerV3:
- ComputerPlayerV1 chooses it's moves randomly.
- ComputerPlayerV2 chooses it's moves programatically.
- ComputerPlayerV3 chooses it's moves based on the probability of winning the game using an AI model.

**Game Setup:** ComputerPlayerV1 [X] vs ComputerPlayerV1 [O]

![Tic-Tac-Toe Game Outcome - ComputerPlayerV1 vs ComputerPlayerV1](/Jupyter-Notebook/GameStats/Output/20230509/TicTacToeGameOutcomeByGameSetup1P_Valid_Test_-_ComputerPlayerV120230509.png)

The number of Player 1 wins are more than 2 times the number of Player 2 wins and 4 times that of Draws. Since both Player 1 and Player 2 chooses their moves randomly, it would suggest that moving first has a significant advantage. This seems consistent with the findings in the entry on [2023-04-28 9:59pm](https://github.com/JosephWee/Tic-Tac-Toe#2023-04-28-959pm-fri---tic-tac-toe-game-stats-part-1)

#### Distribution (%) of Winning Player (Player 1) moves across the 9 cells
![Distribution % of Player 1 moves across the 9 cells](/Jupyter-Notebook/GameStats/Output/20230509/P1_Wins_Cells_occupancy_by_percentage20230509.png)

#### Distribution (%) of Winning Player (Player 2) moves across the 9 cells
![Distribution % of Player 2 moves across the 9 cells](/Jupyter-Notebook/GameStats/Output/20230509/P2_Wins_Cells_occupancy_by_percentage20230509.png)

#### Distribution (%) of Winning Player moves across the 9 cells
![Distribution % of Winning Player moves across the 9 cells](/Jupyter-Notebook/GameStats/Output/20230509/Winner_Cells_occupancy_by_percentage20230509.png)

It seems that when both players are moving randomly, the value of the cells are more apparent. The most valuable cell would be the one in the center followed by the cells in the corners.

**Conclusion**

I believe that the AI model would be more unbiased if the data used is not skewed due to stragems / algorithms employed by specific computer players or human players.

### 2023-05-11 7:28pm Thu - Tic-Tac-Toe Game Stats Part 4
After the creating a ML Model with the unbiased data, ie. without human or computer player stragems / algorithms, I noticed that the new ML Model improved the performance of ComputerPlayerV3, but decreased the accuracy of the Game Outcome prediction.

Player 1 plays `X` while Player 2 plays `O`.
Player 1 always move first.

To recap the difference between ComputerPlayerV1, ComputerPlayerV2 and ComputerPlayerV3:
- ComputerPlayerV1 chooses it's moves randomly.
- ComputerPlayerV2 chooses it's moves programatically.
- ComputerPlayerV3 chooses it's moves based on the probability of winning the game using an AI model.

**Game Setup 1:** ComputerPlayerV1 [X] vs ComputerPlayerV1 [O]

![Tic-Tac-Toe Game Outcome - ComputerPlayerV1 vs ComputerPlayerV1](/Jupyter-Notebook/GameStats/Output/20230511/TicTacToeGameOutcomeByGameSetup2P_Valid_Test_-_P1__ComputerPlayerV1_P2__ComputerPlayerV120230511.png)

The number of Player 1 wins are more than 2 times the number of Player 2 wins and 3.5 times that of Draws. Since both Player 1 and Player 2 chooses their moves randomly, it would suggest that moving first has a significant advantage. This seems consistent with the findings in the entry on [2023-04-28 9:59pm](https://github.com/JosephWee/Tic-Tac-Toe#2023-04-28-959pm-fri---tic-tac-toe-game-stats-part-1).


**Game Setup 2:** ComputerPlayerV1 [X] vs ComputerPlayerV2 [O]

![Tic-Tac-Toe Game Outcome - ComputerPlayerV1 vs ComputerPlayerV2](/Jupyter-Notebook/GameStats/Output/20230511/TicTacToeGameOutcomeByGameSetup2P_Valid_Test_-_P1__ComputerPlayerV1_P2__ComputerPlayerV220230511.png)

The number of Player 2 wins are at least 8 times the number of Player 1 wins and the number of Draws is at least 2 times than the number of Player 1 wins. Since Player 1 chooses it's moves randomly and Player 2 chooses it's moves strategically via an algorithm, it would suggest that choosing one's move strategically has a far greater advantage than moving first. This seems consistent with the findings in the entry on [2023-04-28 9:59pm](https://github.com/JosephWee/Tic-Tac-Toe#2023-04-28-959pm-fri---tic-tac-toe-game-stats-part-1).

**Game Setup 3:** ComputerPlayerV2 [X] vs ComputerPlayerV1 [O]

![Tic-Tac-Toe Game Outcome - ComputerPlayerV2 vs ComputerPlayerV1](/Jupyter-Notebook/GameStats/Output/20230511/TicTacToeGameOutcomeByGameSetup2P_Valid_Test_-_P1__ComputerPlayerV2_P2__ComputerPlayerV120230511.png)

The number of Player 1 wins is more than 16 to 50 times the number of Player 2 wins and Draws. This suggests that choosing one's move strategically and moving first has a very significant advantage. This seems consistent with the findings in the entry on [2023-04-28 9:59pm](https://github.com/JosephWee/Tic-Tac-Toe#2023-04-28-959pm-fri---tic-tac-toe-game-stats-part-1).

**Game Setup 4:** ComputerPlayerV2 [X] vs ComputerPlayerV2 [O]

![Tic-Tac-Toe Game Outcome - ComputerPlayerV2 vs ComputerPlayerV2](/Jupyter-Notebook/GameStats/Output/20230511/TicTacToeGameOutcomeByGameSetup2P_Valid_Test_-_P1__ComputerPlayerV2_P2__ComputerPlayerV220230511.png)

There are no Player 1 wins and the number of Player 2 wins are slightly lower than the number of Draws. This suggests that when both players are moving strategically, moving second has a significant advantage. This seems consistent with the findings in the entry on [2023-04-28 9:59pm](https://github.com/JosephWee/Tic-Tac-Toe#2023-04-28-959pm-fri---tic-tac-toe-game-stats-part-1).

#### Distribution (%) of Winning Player (Player 1) moves across the 9 cells
![Distribution % of Player 1 moves across the 9 cells](/Jupyter-Notebook/GameStats/Output/20230511/P1_Wins_Cells_occupancy_by_percentage20230511.png)

#### Distribution (%) of Winning Player (Player 2) moves across the 9 cells
![Distribution % of Player 2 moves across the 9 cells](/Jupyter-Notebook/GameStats/Output/20230511/P2_Wins_Cells_occupancy_by_percentage20230511.png)

#### Distribution (%) of Winning Player moves across the 9 cells
![Distribution % of Winning Player moves across the 9 cells](/Jupyter-Notebook/GameStats/Output/20230511/Winner_Cells_occupancy_by_percentage20230511.png)

It seems that when both players are moving randomly, the value of the cells are more apparent. The most valuable cell would be the one in the center followed by the cells in the corners.

**Conclusion**

`ComputerPlayerV3` would benefit from an AI Model that is based on Player 1 and Player 2 winning moves when both players chooses their moves `randomly`. While the `Game Outcome Prediction` would be better served by an AI Model based on Player 1 winning, Player 2 winning and Draws when the players are moving `randomly` and also when they are choosing their moves `strategically`.
