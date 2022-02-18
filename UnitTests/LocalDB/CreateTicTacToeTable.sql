USE [$(DatabaseName)];

CREATE TABLE [dbo].[TicTacToeData] (
    [Id]          INT            NOT NULL IDENTITY,
    [CreatedDate] DATETIME2 (7)  NOT NULL,
    [InstanceId]  NVARCHAR (200) NOT NULL,
    [GridSize]    INT            NOT NULL,
    [MoveNumber]  INT            NULL,
    [CellIndex]   INT            NOT NULL,
    [CellContent] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);