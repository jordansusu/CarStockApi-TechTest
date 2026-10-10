// Represent the JWT options

namespace CarStockApi.Models;

public sealed class JwtOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public int ExpiryInMinutes { get; set; } = 5;
}