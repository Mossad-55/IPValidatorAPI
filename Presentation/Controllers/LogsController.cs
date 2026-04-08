using Application.Interfaces;
using Application.RequestFeatures;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/logs")]
[ApiController]
public class LogsController : ControllerBase
{
    private readonly IServiceManager _service;
    public LogsController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet("blocked-attempts")]
    public async Task<IActionResult> GetLogs([FromQuery] PaginationRequest request)
    {
        var result = await _service.LogService.GetAllLogsAsync(request);

        return Ok(result);
    }
}
