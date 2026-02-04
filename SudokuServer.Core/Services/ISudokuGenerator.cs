using SudokuServer.Core.Models;

namespace SudokuServer.Core.Services
{
    public interface ISudokuGenerator
    {
        Board Generate(int size, string difficulty = "Easy");
    }
}
