using SudokuServer.Core.Models;

namespace SudokuServer.Core.Services
{
    public interface ISudokuSolver
    {
        bool Solve(Board board); // modifies board to solution if solvable
        int CountSolutions(Board board, int maxCount = 2); // used to check uniqueness
    }
}
