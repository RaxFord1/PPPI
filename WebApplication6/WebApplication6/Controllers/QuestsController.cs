using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.DTO;
using PracticeAPI.DTO.Quest;
using PracticeAPI.Models;
using PracticeAPI.Services.QuestService;

namespace PracticeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestsController : ControllerBase
    {
        private readonly IQuestService _questService;

        public QuestsController(IQuestService questService)
        {
            _questService = questService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Quest>>> Get(Guid id)
        {
            var quest = await _questService.Get(id);
            if (quest == null || quest.ValueCount == 0)
            {
                return NotFound(quest);
            }

            return Ok(quest);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<Quest>>> GetAll()
        {
            var quests = await _questService.GetAll();
            return Ok(quests);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Quest>>> Post([FromBody] CreateQuestRequest request)
        {
            var quest = await _questService.Post(request);
            return Ok(quest);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Quest>>> Put(Guid id, [FromBody] Quest quest)
        {
            var response = await _questService.Put(id, quest);

            if (response == null || !response.Success) {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<Quest>>> Delete(Guid id)
        {
            await _questService.Delete(id);
            return NoContent();
        }
    }
}
