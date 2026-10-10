// Custom base endpoints

using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CarStockApi.Auth;

// For AddCar, SearchCar
public abstract class DealerEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse> where TRequest : notnull
{
    protected int DealerId => int.TryParse(
        User.FindFirstValue(ClaimTypes.NameIdentifier),
        out var dealerId)
            ? dealerId
            : throw new UnauthorizedAccessException("Dealer ID is missing or invalid");
    public override void Configure()
    {
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }
}

// For UpdateStock
public abstract class DealerEndpoint<TRequest> : Endpoint<TRequest> where TRequest : notnull
{
    protected int DealerId => int.TryParse(
        User.FindFirstValue(ClaimTypes.NameIdentifier),
        out var dealerId)
            ? dealerId
            : throw new UnauthorizedAccessException("Dealer ID is missing or invalid");
    public override void Configure()
    {
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }
}

// For GetCar, ListCar
public abstract class DealerEndpointWithoutRequest<TResponse> : EndpointWithoutRequest<TResponse>
{
    protected int DealerId => int.TryParse(
        User.FindFirstValue(ClaimTypes.NameIdentifier),
        out var dealerId)
            ? dealerId
            : throw new UnauthorizedAccessException("Dealer ID is missing or invalid");
    public override void Configure()
    {
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }
}

// For RemoveCar
public abstract class DealerEndpointWithoutRequest : EndpointWithoutRequest
{
    protected int DealerId => int.TryParse(
        User.FindFirstValue(ClaimTypes.NameIdentifier),
        out var dealerId)
            ? dealerId
            : throw new UnauthorizedAccessException("Dealer ID is missing or invalid");
    public override void Configure()
    {
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }
}
