using Application.DTOs;
using Application.Interfaces;
using Application.RequestFeatures;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/countries")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly IServiceManager _service;

    public CountriesController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost("block")]
    public async Task<IActionResult> BlockCountry([FromBody] BlockCountryDto dto)
    {
        await _service.CountryService.AddBlockedCountryAsync(dto);

        return Ok(new { message = "Blocked country successfully." });
    }

    [HttpGet("blocked")]
    public async Task<IActionResult> GetBlockedCountries([FromQuery] PaginationRequest request)
    {
        var result = await _service.CountryService.GetBlockedCountreisAsync(request);

        return Ok(result);
    }

    [HttpDelete("block/{countryCode}")]
    public async Task<IActionResult> DeleteBlockedCountry(string countryCode)
    {
        await _service.CountryService.RemoveBlockedCountryAsync(countryCode);

        return NoContent();
    }

    [HttpPost("temporal-block")]
    public async Task<IActionResult> TemporalBlock([FromBody] TemporalBlockDto dto)
    {
        await _service.CountryService.AddTemporalBlockAsync(dto);

        return Ok(new { message = $"Country with code: {dto.CountryCode} has been temporarly blocked for {dto.DurationInMinutes} minute/s." });
    }
}
