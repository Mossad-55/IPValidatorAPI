namespace Domain.Entities;

public class TemporalBlockedCountry : BlockedCountry
{
    public DateTime ExpiresAt { get; set; }
}
