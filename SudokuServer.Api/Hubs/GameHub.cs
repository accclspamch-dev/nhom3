using Microsoft.AspNetCore.SignalR;
using SudokuServer.Core.Managers;
using SudokuServer.Core.Models;

namespace SudokuServer.Api.Hubs
{
    public class GameHub : Hub
    {
        private readonly GameRoomManager _rooms;

        public GameHub(GameRoomManager rooms)
        {
            _rooms = rooms;
        }

        public async Task JoinRoom(string roomId, string playerName)
        {
            var room = _rooms.GetRoom(roomId) ?? _rooms.CreateRoom(roomId, 9);
            var player = new PlayerSession { ConnectionId = Context.ConnectionId, Name = playerName, RemainingTimeSeconds = 600 };
            _rooms.AddPlayer(roomId, player);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("PlayerJoined", playerName);
        }

        public async Task MakeMove(string roomId, int row, int col, int value)
        {
            var result = _rooms.MakeMove(roomId, Context.ConnectionId, row, col, value);
            if (result.Success)
            {
                await Clients.Group(roomId).SendAsync("BoardDelta", row, col, value, Context.ConnectionId);
                if (result.GameEnded)
                {
                    await Clients.Group(roomId).SendAsync("GameEnded", result.WinnerConnectionId);
                    _rooms.CloseRoom(roomId);
                }
            }
            else
            {
                await Clients.Caller.SendAsync("InvalidMove", result.Reason);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // remove player from any room they were in (simple approach)
            // iterate rooms and remove connection
            await base.OnDisconnectedAsync(exception);
        }
    }
}
