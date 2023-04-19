# Tick-Tac-Toe
## Objectives
The Tic-Tac-Toe project was started in 16 Feb 2022 as a way to learn about Machine Learning.

## Overview
The current implementation has 2 versions of Computer Players (ComputerPlayerV1 & ComputerPlayerV2).
Both these versions computer players that chooses their moves programatically.
It is one of my goals to eventually create a Computer Player that chooses their moves based on the probability of winning the game using an AI model.

![Tic-Tac-Toe Project Architectural Overview ](/Documentation/Tic-Tac-Toe-Overview-compact.jpeg)

## Quickstart
### Web API
**connectionStrings.json**
Replace the connection string named "TicTacToeDataConnString" with the actual value of your connection string.

For example:
> {
>     "ConnectionStrings": {
>         "TicTacToeDataConnString": "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\DEV\\LocalDB\\TicTacToeData.mdf;Integrated Security=True;Connect Timeout=30"
>     }
> }

For more information see:
- https://learn.microsoft.com/en-us/ef/core/miscellaneous/connection-strings
- https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0
- https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0#file-configuration-provider