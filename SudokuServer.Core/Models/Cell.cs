namespace SudokuServer.Core.Models
{
    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Value { get; set; } // 0 nếu rỗng
        public bool IsGiven { get; set; }
    }
}
