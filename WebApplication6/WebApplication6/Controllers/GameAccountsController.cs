using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.DTO.GameAccount;
using PracticeAPI.Models;
using PracticeAPI.Services.GameAccountService;

namespace PracticeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameAccountsController : ControllerBase
    {
        private readonly IGameAccountService _gameAccountService;

        public GameAccountsController(IGameAccountService gameAccountService)
        {
            _gameAccountService = gameAccountService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<GameAccount>>> Get(Guid id)
        {
            var gameAccount = await _gameAccountService.Get(id);
            if (gameAccount == null || gameAccount.ValueCount == 0)
            {
                return NotFound(gameAccount);
            }

            return Ok(gameAccount);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<GameAccount>>> GetAll()
        {
            var gameAccounts = await _gameAccountService.GetAll();
            return Ok(gameAccounts);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<GameAccount>>> Post([FromBody] CreateGameAccountRequest request)
        {
            var gameAccount = await _gameAccountService.Post(request);
            return Ok(gameAccount);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<GameAccount>>> Put(Guid id, [FromBody] UpdateGameAccountRequest ugar)
        {
            var response = await _gameAccountService.Put(id, ugar);

            if (response == null || !response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<GameAccount>>> Delete(Guid id)
        {
            await _gameAccountService.Delete(id);
            return NoContent();
        }
    }
}
