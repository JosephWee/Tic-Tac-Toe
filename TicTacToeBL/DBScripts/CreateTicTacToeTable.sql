--USE [$(DatabaseName)];

DECLARE @DO_MIGRATION BIT = (
	SELECT 1 FROM SYS.COLUMNS
	WHERE NAME = N'GridSize'
	AND OBJECT_ID = OBJECT_ID(N'dbo.TicTacToeData')
)

IF (@DO_MIGRATION=1)
	BEGIN
		EXEC sp_rename 'dbo.TicTacToeData', 'TicTacToeDataOld';
		--DROP TABLE [dbo].[TicTacToeData]
		--DROP TABLE [dbo].[TicTacToeGames]
	END

CREATE TABLE [dbo].[TicTacToeGames]
(
	[InstanceId]    BIGINT        NOT NULL PRIMARY KEY,
	[Description]   NVARCHAR(500) NOT NULL,
	[GridSize]      INT           NOT NULL,
	[Status]        INT               NULL DEFAULT NULL,
    [CreatedDate]   DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME(),
    [CompletedDate] DATETIME2         NULL DEFAULT NULL
)

CREATE TABLE [dbo].[TicTacToeData]
(
    [Id]          BIGINT        NOT NULL IDENTITY,
    [CreatedDate] DATETIME2(7)  NOT NULL DEFAULT SYSUTCDATETIME(),
    [InstanceId]  BIGINT        NOT NULL,
    [MoveNumber]  INT           NOT NULL,
    [CellIndex]   INT           NOT NULL,
    [CellContent] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT FK_TicTacToeGames_TicTacToeData 
		FOREIGN KEY (InstanceId) REFERENCES dbo.TicTacToeGames (InstanceId)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)

IF (@DO_MIGRATION=1)
	BEGIN
		DECLARE @InstanceId BIGINT = (SELECT DATEDIFF(S, '1956-01-01', GETUTCDATE()))
		INSERT INTO dbo.TicTacToeGames
		(
			[InstanceId],
			[Description],
			[GridSize],
			[CreatedDate]
		)
		SELECT
			(SELECT @InstanceId + T2.[ROWNUM]) AS [InstanceId],
			T2.[GameId] AS [Description],
			T2.[Size] AS [GridSize],
			T2.[Created] AS [CreatedDate]
		FROM
		(
			SELECT
				ROW_NUMBER() OVER (ORDER BY T1.[CreatedDate] ASC) AS ROWNUM,
				T1.[InstanceId] AS GameId,
				T1.[GridSize] AS Size,
				T1.[CreatedDate] AS Created,
				T1.IDCOUNT,
				T1.CICOUNT
			FROM
			(
				SELECT
					[InstanceId],
					MAX([GridSize]) AS GridSize,
					MIN([CreatedDate]) AS CreatedDate,
					COUNT([Id]) AS IDCOUNT,
					COUNT([CellIndex]) AS CICOUNT
				FROM
					dbo.TicTacToeDataOld
				WHERE
					[MoveNumber] = 1
				GROUP BY
					[InstanceId]
			) AS T1
			WHERE
				T1.IDCOUNT = 9
				AND T1.CICOUNT = 9
				AND T1.GridSize = 3
		) AS T2
		
		INSERT INTO dbo.TicTacToeData
		(
			[CreatedDate],
			[InstanceId],
			[MoveNumber],
			[CellIndex],
			[CellContent]
		)
		SELECT
			G.CreatedDate,
			G.InstanceId,
			O.MoveNumber,
			O.CellIndex,
			O.CellContent
		FROM
			dbo.[TicTacToeDataOld] AS O
		INNER JOIN dbo.[TicTacToeGames] AS G
		ON O.InstanceId = G.Description
		ORDER BY
			O.InstanceId,
			O.MoveNumber
			
	END