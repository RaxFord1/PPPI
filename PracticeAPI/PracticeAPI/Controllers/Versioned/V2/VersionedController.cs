using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.Services.VersionedServices.V2;

namespace PracticeAPI.Controllers.Versioned.V2;

[ApiVersion("2.0")]
[ApiController]
[ApiExplorerSettings(GroupName = "v2")]
[Route("api/v2/[controller]")]
public class VersionedController : ControllerBase
{
    private readonly IStringService _stringService;

    public VersionedController(IStringService stringService)
    {
        _stringService = stringService;
    }

    [HttpGet, Authorize]
    public async Task<IActionResult> Get()
    {
        return Ok(await _stringService.GetSomeText());
    }
}
