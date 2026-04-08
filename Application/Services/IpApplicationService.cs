using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System.Net;

namespace Application.Services;

public class IpApplicationService
{
    private readonly IIpService _ipService;
    private readonly IRepositoryManager _repo;

    public IpApplicationService(IIpService service, IRepositoryManager repo)
    {
        _repo = repo;
        _ipService = service;
    }

    public async Task<IpLookupResponseDto> LookupAsync(string ip)
    {
        if (!IPAddress.TryParse(ip, out _))
            throw new ConflictException("Invalid IP Address.");

        var result = await _ipService.GetCountryByIpAsync(ip)!;
        if (string.IsNullOrEmpty(result.CountryCode) 
            && string.IsNullOrEmpty(result.CountryName))
            throw new Exception("External API failed.");

        return new IpLookupResponseDto
        {
            IpAddress = ip,
            CountryCode = result.CountryCode,
            CountryName = result.CountryName
        };
    }

    public async Task<bool> CheckIfBlockedAsync(string ip, string userAger)
    {
        var result = await _ipService.GetCountryByIpAsync(ip)!;
        if (string.IsNullOrEmpty(result.CountryName)
            && string.IsNullOrEmpty(result.CountryCode))
            throw new Exception("IP lookup failed.");

        var isBlocked = await _repo.BlockedCountry.ExistsAsync(result.CountryCode);

        await _repo.Log.AddAsync(new LogEntry
        {
            IpAddess = ip,
            CountryCode = result.CountryCode,
            Timestamp = DateTime.UtcNow,
            IsBlocked = isBlocked,
            UserAgent = userAger
        });

        return isBlocked;
    }
}
