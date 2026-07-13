using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using WorldRank.Application.Interfaces;
using WorldRank.Domain.Entities;

namespace WorldRank.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
       
        private readonly IPlayerRepository _playerRepository;
        public PlayersController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = _playerRepository.GetAllPlayers();
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("search_player/{playerId:guid}")]
        public async Task<IActionResult> GetAll(int playerId)
        {
            try
            {
                var result = _playerRepository.FindPlayer(playerId);
                if (result == null) return NotFound();

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
