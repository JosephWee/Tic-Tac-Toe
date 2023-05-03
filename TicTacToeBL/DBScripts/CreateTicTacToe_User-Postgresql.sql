\c "TicTacToeDb"

CREATE USER "TicTacToeUser" WITH PASSWORD 'TicTacToePassword';

GRANT ALL PRIVILEGES ON "TicTacToeDb" TO "TicTacToeUser";

\quit