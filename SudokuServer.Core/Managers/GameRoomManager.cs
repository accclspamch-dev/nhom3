using SudokuServer.Core.Models;
using SudokuServer.Core.Services;
using System.Collections.Concurrent;

namespace SudokuServer.Core.Managers
{
    public class GameRoomManager
    {
        private readonly ConcurrentDictionary<string, GameRoom> _rooms = new();
        private readonly ISudokuGenerator _generator;
        private readonly ISudokuSolver _solver;

        public GameRoomManager(ISudokuGenerator generator, ISudokuSolver solver)
        {
            _generator = generator;
            _solver = solver;
        }

        public GameRoom CreateRoom(string roomId, int size = 9, string difficulty = "Easy")
        {
            var board = _generator.Generate(size, difficulty);
            var room = new GameRoom { RoomId = roomId, Board = board, IsStarted = false };
            _rooms[roomId] = room;
            return room;
        }

        public bool AddPlayer(string roomId, PlayerSession player)
        {
            if (!_rooms.TryGetValue(roomId, out var room)) return false;
            return room.Players.TryAdd(player.ConnectionId, player);
        }

        public bool RemovePlayer(string roomId, string connectionId)
        {
            if (!_rooms.TryGetValue(roomId, out var room)) return false;
            return room.Players.TryRemove(connectionId, out _);
        }

        public MoveResult MakeMove(string roomId, string connectionId, int row, int col, int value)
        {
            if (!_rooms.TryGetValue(roomId, out var room)) return MoveResult.Fail("Room not found");
            if (row < 0 || col < 0 || row >= room.Board.Size || col >= room.Board.Size)
                return MoveResult.Fail("Invalid coordinates");

            var cell = room.Board.Cells[row, col];
            if (cell.IsGiven) return MoveResult.Fail("Cell is given");

            // apply move
            cell.Value = value;

            // quick validity check
            if (!room.Board.IsValid())
            {
                cell.Value = 0; // revert
                return MoveResult.Fail("Move violates Sudoku rules");
            }

            // check win
            if (room.Board.IsComplete())
            {
                if (room.Board.IsValid())
                {
                    return new MoveResult { Success = true, GameEnded = true, WinnerConnectionId = connectionId };
                }
            }

            return MoveResult.Ok();
        }

        public GameRoom GetRoom(string roomId) => _rooms.TryGetValue(roomId, out var r) ? r : null;

        public void CloseRoom(string roomId) => _rooms.TryRemove(roomId, out _);
    }
}
