// Defines the credentials required to authenticate a dealer.

namespace CarStockApi.Features.Auth;

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}