using System.Collections.Generic;

namespace SudokuServer.Core.Models
{
    public class PlayerSession
    {
        public string ConnectionId { get; set; }
        public string PlayerId { get; set; } // optional user id
        public string Name { get; set; }
        public int RemainingTimeSeconds { get; set; }
        public List<string> Items { get; set; } = new();
    }
}