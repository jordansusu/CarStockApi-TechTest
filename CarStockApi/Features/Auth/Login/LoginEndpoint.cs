// Handle dealer login
using CarStockApi.Auth;
using CarStockApi.Database;
using CarStockApi.Models;
using Dapper;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace CarStockApi.Features.Auth.Login;

public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly ITokenService _tokenService;
    private readonly PasswordHasher<Dealer> _passwordHasher = new();
    public LoginEndpoint(IDbConnectionFactory dbConnectionFactory, ITokenService tokenService)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _tokenService = tokenService;
    }

    public override void Configure()
    {
        Post("/api/auth/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var dealer = await connection.QuerySingleOrDefaultAsync<Dealer>(
          """
            SELECT Id, Username, PasswordHash
            FROM Dealers
            WHERE Username = @Username
          """,
          new { req.Username }  
        );

        // Reject unknown usernames
        if (dealer is null)
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }

        // Verify the password against the stored hash one
        var result = _passwordHasher.VerifyHashedPassword(
            dealer,
            dealer.PasswordHash,
            req.Password
        );
        if (result == PasswordVerificationResult.Failed)
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }

        // Generate a JWT for the authenticated dealer
        var token = _tokenService.GenerateToken(
            dealer.Id,
            dealer.Username
        );
        // Return JWT to the client
        await Send.OkAsync(
            new LoginResponse { Token = token },
            cancellation: ct
        );
    }
}