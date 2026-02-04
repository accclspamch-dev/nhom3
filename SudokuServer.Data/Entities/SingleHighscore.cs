using System;

namespace SudokuServer.Data.Entities
{
    public class SingleHighscore
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public string Difficulty { get; set; }
        public int TimeTakenSeconds { get; set; }
        public DateTime DatePlayed { get; set; }
    }
}
