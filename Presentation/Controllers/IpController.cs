using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/ip")]
[ApiController]
public class IpController : ControllerBase
{
    private readonly IpApplicationService _service;

    public IpController(IpApplicationService service)
    {
        _service = service;
    }

    [HttpGet("lookup")]
    public async Task<IActionResult> Lookup([FromQuery] string? ipAddress)
    {
        var ip = ipAddress ?? HttpContext.Connection.RemoteIpAddress?.ToString();

        var result = await _service.LookupAsync(ip!);

        return Ok(result);
    }

    [HttpGet("check-block")]
    public async Task<IActionResult> CheckBlock([FromQuery] string? ipAddress)
    {
        var ip = ipAddress ?? HttpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = Request.Headers["User-Agent"].ToString();

        var isBlocked = await _service.CheckIfBlockedAsync(ip!, userAgent);

        return Ok(new { isBlocked = isBlocked });
    }
}
