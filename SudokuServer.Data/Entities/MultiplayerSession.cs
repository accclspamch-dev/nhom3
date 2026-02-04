using System;

namespace SudokuServer.Data.Entities
{
    public class MultiplayerSession
    {
        public int Id { get; set; }
        public string RoomId { get; set; }
        public string PlayersJson { get; set; } // simple JSON snapshot
        public string Winner { get; set; }
        public int DurationSeconds { get; set; }
        public string ItemsUsedJson { get; set; }
        public DateTime DatePlayed { get; set; }
    }
}
