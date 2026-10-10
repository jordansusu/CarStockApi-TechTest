// Defines the contract for generating JWT tokens.

namespace CarStockApi.Auth;

public interface ITokenService
{
    string GenerateToken(int dealerId, string username);
}