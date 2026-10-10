// Handles DELETE /api/card/{id} request.

using CarStockApi.Auth;
using CarStockApi.Database;
using Dapper;
using FastEndpoints;

namespace CarStockApi.Features.Cars.RemoveCar;

public class RemoveCarEndpoint : DealerEndpointWithoutRequest
{
    private readonly IDbConnectionFactory _connectionFactory;
    public RemoveCarEndpoint(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public override void Configure()
    {
        base.Configure();
        Delete("/api/cars/{id}");
    }

    public override async Task HandleAsync (CancellationToken ct)
    {
        var carId = Route<int>("id");

        using var connection = _connectionFactory.CreateConnection();

        const string sql = """
                DELETE FROM Cars
                WHERE Id = @CarId
                    AND DealerId = @DealerId;
            """;
        
        var rowAffected = await connection.ExecuteAsync(
            sql,
            new
            {
                CarId = carId,
                DealerId
            }
        );

        if (rowAffected == 0)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}