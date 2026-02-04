using Microsoft.EntityFrameworkCore;
using SudokuServer.Data.Entities;

namespace SudokuServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SingleHighscore> SingleHighscores { get; set; }
        public DbSet<MultiplayerSession> MultiplayerSessions { get; set; }
    }
}
