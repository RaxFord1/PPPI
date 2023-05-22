using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.Services.VersionedServices.V1;

namespace PracticeAPI.Controllers.Versioned.V1;

[ApiVersion("1.0", Deprecated = true)]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v1/[controller]")]
public class VersionedController : ControllerBase
{
    private readonly INumberService _numberService;

    public VersionedController(INumberService numberService)
    {
        _numberService = numberService;
    }

    [HttpGet, Authorize]
    [Obsolete("This method is deprecated in version 1.0. Please use the updated version")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _numberService.GetRandomInteger());
    }
}
