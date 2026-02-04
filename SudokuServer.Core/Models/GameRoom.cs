using System.Collections.Concurrent;

namespace SudokuServer.Core.Models
{
    public class GameRoom
    {
        public string RoomId { get; set; }
        public Board Board { get; set; }
        public ConcurrentDictionary<string, PlayerSession> Players { get; set; } = new();
        public bool IsStarted { get; set; }
    }
}
