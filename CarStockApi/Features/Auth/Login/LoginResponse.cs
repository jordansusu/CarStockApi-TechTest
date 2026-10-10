// Defines the response returned after successful authentication.

namespace CarStockApi.Features.Auth.Login;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
}