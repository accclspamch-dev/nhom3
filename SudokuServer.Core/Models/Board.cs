using System;

namespace SudokuServer.Core.Models
{
    public class Board
    {
        public int Size { get; set; }
        public Cell[,] Cells { get; set; }

        public Board(int size)
        {
            if (size <= 0) throw new ArgumentException("Size must be > 0");
            Size = size;
            Cells = new Cell[size, size];
            for (int r = 0; r < size; r++)
                for (int c = 0; c < size; c++)
                    Cells[r, c] = new Cell { Row = r, Col = c, Value = 0, IsGiven = false };
        }

        public bool IsComplete()
        {
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    if (Cells[r, c].Value == 0) return false;
            return true;
        }

        // Simple validity check: no duplicate in row/col/box (works for standard square boxes)
        public bool IsValid()
        {
            int n = Size;
            int boxSize = (int)Math.Sqrt(n);
            if (boxSize * boxSize != n) boxSize = 0; // non-square boxes not handled here

            // rows
            for (int r = 0; r < n; r++)
            {
                var seen = new bool[n + 1];
                for (int c = 0; c < n; c++)
                {
                    int v = Cells[r, c].Value;
                    if (v == 0) continue;
                    if (v < 0 || v > n) return false;
                    if (seen[v]) return false;
                    seen[v] = true;
                }
            }

            // cols
            for (int c = 0; c < n; c++)
            {
                var seen = new bool[n + 1];
                for (int r = 0; r < n; r++)
                {
                    int v = Cells[r, c].Value;
                    if (v == 0) continue;
                    if (seen[v]) return false;
                    seen[v] = true;
                }
            }

            // boxes (only if perfect square)
            if (boxSize > 0)
            {
                for (int br = 0; br < boxSize; br++)
                    for (int bc = 0; bc < boxSize; bc++)
                    {
                        var seen = new bool[n + 1];
                        for (int r = br * boxSize; r < (br + 1) * boxSize; r++)
                            for (int c = bc * boxSize; c < (bc + 1) * boxSize; c++)
                            {
                                int v = Cells[r, c].Value;
                                if (v == 0) continue;
                                if (seen[v]) return false;
                                seen[v] = true;
                            }
                    }
            }

            return true;
        }
    }
}
