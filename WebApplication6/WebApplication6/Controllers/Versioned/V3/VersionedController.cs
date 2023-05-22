using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.Services.VersionedServices.V3;

namespace PracticeAPI.Controllers.Versioned.V3;

[ApiVersion("3.0")]
[ApiController]
[ApiExplorerSettings(GroupName = "v3")]
[Route("api/v3/[controller]")]
public class VersionedController : ControllerBase
{
    private readonly IExcelFileService _excelFileService;

    public VersionedController(IExcelFileService excelFileService)
    {
        _excelFileService = excelFileService;
    }

    [HttpGet, Authorize]
    public async Task<IActionResult> Get()
    {
        var workbook = await _excelFileService.Get();
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return File(
            content,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "excelFile.xlsx");
    }
}
