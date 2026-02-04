using Microsoft.AspNetCore.Mvc;
using SudokuServer.Data;
using SudokuServer.Data.Entities;

namespace SudokuServer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HighscoreController : ControllerBase
    {
        private readonly AppDbContext _db;
        public HighscoreController(AppDbContext db) { _db = db; }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SingleHighscore model)
        {
            model.DatePlayed = DateTime.UtcNow;
            _db.SingleHighscores.Add(model);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("top")]
        public IActionResult Top([FromQuery] string difficulty)
        {
            var top = _db.SingleHighscores
                         .Where(h => h.Difficulty == difficulty)
                         .OrderBy(h => h.TimeTakenSeconds)
                         .Take(10)
                         .ToList();
            return Ok(top);
        }
    }
}
