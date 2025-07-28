using TodoApi.Services;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService _playerService;

        public PlayerController(PlayerService playerService)
        {
            _playerService = playerService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Player>>> GetPlayers()
        {
            var players = await _playerService.GetAllAsync();
            return Ok(players);
        }


        [HttpGet("top5")]
        public async Task<ActionResult<List<Player>>> GetTop5()
        {
            var players = await _playerService.GetTop5PlayersAsync();
            return Ok(players);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(String id)
        {
            var player = await _playerService.GetByIdAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer(Player player)
        {
            await _playerService.CreateAsync(player);
            return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, player);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(string id, Player player)
        {
            var existingPlayer = await _playerService.GetByIdAsync(id);
            if (existingPlayer == null) return NotFound();

            await _playerService.UpdateAsync(id, player);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(string id)
        {
            var jugador = await _playerService.GetByIdAsync(id);
            if (jugador == null) return NotFound();

            await _playerService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/addpoints")]
        public async Task<IActionResult> AddPoints(string id, [FromBody] int newScore)
        {
            var existingPlayer = await _playerService.GetByIdAsync(id);
            if (existingPlayer == null) return NotFound();

            if (newScore > existingPlayer.Puntuacion)
            {
                existingPlayer.Puntuacion = newScore;
                await _playerService.UpdateAsync(id, existingPlayer);
                return Ok(new { updated = true, newHighScore = newScore });
            }
            return Ok(new { updated = false, currentHighScore = existingPlayer.Puntuacion });

            
        }
    }
}