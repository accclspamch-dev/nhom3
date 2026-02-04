using SudokuServer.Core.Models;
using System;

namespace SudokuServer.Core.Services
{
    // Simple generator stub: returns empty board or can be extended to full generator.
    public class BacktrackingGenerator : ISudokuGenerator
    {
        public Board Generate(int size, string difficulty = "Easy")
        {
            // For now return an empty board; replace with full generator later.
            var board = new Board(size);
            // TODO: implement full backtracking generator that fills board and removes cells by difficulty.
            return board;
        }
    }
}
