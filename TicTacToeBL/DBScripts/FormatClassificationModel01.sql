select
	t.id,
	CONCAT('''', CONVERT(VARCHAR(23), t.CreatedDate, 121), '''') as sCreatedDate,
	CONCAT('''', t.InstanceId, '''') as sInstanceId,
	t.MoveNumber,
	t.Cell0,
	t.Cell1,
	t.Cell2,
	t.Cell3,
	t.Cell4,
	t.Cell5,
	t.Cell6,
	t.Cell7,
	t.Cell8,
	t.GameResultCode,
	t.Draw,
	t.Player1Wins,
	t.Player2Wins
from
	dbo.TicTacToeClassificationModel01 t