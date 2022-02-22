CREATE TABLE [dbo].[TicTacToeClassificationModel01] (
    [Id]                INT            NOT NULL IDENTITY,
    [CreatedDate]       DATETIME2 (7)  NOT NULL,
    [InstanceId]        NVARCHAR (200) NOT NULL,
    [MoveNumber]        INT            NOT NULL,
    [Cell0]             INT            NOT NULL,
    [Cell1]             INT            NOT NULL,
    [Cell2]             INT            NOT NULL,
    [Cell3]             INT            NOT NULL,
    [Cell4]             INT            NOT NULL,
    [Cell5]             INT            NOT NULL,
    [Cell6]             INT            NOT NULL,
    [Cell7]             INT            NOT NULL,
    [Cell8]             INT            NOT NULL,
    [GameResultCode]    INT            NOT NULL,
    [Draw]              BIT            NOT NULL,
    [Player1Wins]       BIT            NOT NULL,
    [Player2Wins]       BIT            NOT NULL
    PRIMARY KEY CLUSTERED ([Id] ASC)
);