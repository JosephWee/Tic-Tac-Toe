SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TicTacToeClassificationModel02] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [InstanceId]     NVARCHAR (200) NOT NULL,
    [MoveNumber]     INT            NULL,
    [CellIndex]      INT            NOT NULL,
    [CellContent]    INT            NOT NULL,
	[GameResultCode] INT            NOT NULL
);


