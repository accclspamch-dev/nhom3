namespace SudokuServer.Core.Models
{
    public class MoveResult
    {
        public bool Success { get; set; }
        public string Reason { get; set; }
        public bool GameEnded { get; set; }
        public string WinnerConnectionId { get; set; }

        public static MoveResult Fail(string reason) => new MoveResult { Success = false, Reason = reason };
        public static MoveResult Ok() => new MoveResult { Success = true };
    }
}
