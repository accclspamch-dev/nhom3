using SudokuServer.Core.Models;

namespace SudokuServer.Core.Services
{
    // Simple backtracking solver for standard square Sudoku.
    public class BacktrackingSolver : ISudokuSolver
    {
        public bool Solve(Board board)
        {
            return SolveInternal(board);
        }

        public int CountSolutions(Board board, int maxCount = 2)
        {
            int count = 0;
            CountInternal(board, ref count, maxCount);
            return count;
        }

        private bool SolveInternal(Board board)
        {
            int n = board.Size;
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (board.Cells[r, c].Value == 0)
                    {
                        for (int v = 1; v <= n; v++)
                        {
                            board.Cells[r, c].Value = v;
                            if (board.IsValid() && SolveInternal(board)) return true;
                            board.Cells[r, c].Value = 0;
                        }
                        return false;
                    }
                }
            }
            return board.IsValid();
        }

        private void CountInternal(Board board, ref int count, int maxCount)
        {
            if (count >= maxCount) return;
            int n = board.Size;
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (board.Cells[r, c].Value == 0)
                    {
                        for (int v = 1; v <= n; v++)
                        {
                            board.Cells[r, c].Value = v;
                            if (board.IsValid()) CountInternal(board, ref count, maxCount);
                            board.Cells[r, c].Value = 0;
                            if (count >= maxCount) return;
                        }
                        return;
                    }
                }
            }
            // found a solution
            count++;
        }
    }
}
