using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracticeAPI.DTO;
using PracticeAPI.DTO.Character;
using PracticeAPI.Models;
using PracticeAPI.Services.CharacterService;

namespace PracticeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharactersController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Character>>> Get(Guid id)
        {
            var character = await _characterService.Get(id);
            if (character == null || character.ValueCount == 0)
            {
                return NotFound(character);
            }

            return Ok(character);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<Character>>> GetAll()
        {
            return Ok(await _characterService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Character>>> Post([FromBody] CreateCharacterRequest request)
        {
            var character = await _characterService.Post(request);
            return Ok(character);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Character>>> Put(Guid id, [FromBody] Character character)
        {
            var response = await _characterService.Put(id, character);

            if (response == null || !response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<Character>>> Delete(Guid id)
        {
            await _characterService.Delete(id);
            return NoContent();
        }
    }
}
