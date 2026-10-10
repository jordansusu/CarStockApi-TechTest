// Handles PUT /api/cars/{id}/stock request

using CarStockApi.Auth;
using CarStockApi.Database;
using Dapper;
using FastEndpoints;

namespace CarStockApi.Features.Cars.UpdateStock;

public class UpdateStockEndpoint : DealerEndpoint<UpdateStockRequest>
{
    private readonly IDbConnectionFactory _connectionFactory;
    public UpdateStockEndpoint(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public override void Configure()
    {
        base.Configure();
        Put("/api/cars/{id}/stock");
    }

    public override async Task HandleAsync(UpdateStockRequest req, CancellationToken ct)
    {
        var carId = Route<int>("id");
        using var connection = _connectionFactory.CreateConnection();
        const string sql = """
            UPDATE Cars
            SET Stock = @Stock
            WHERE Id = @CarId
                AND DealerId = @DealerId;
            """;

        var rowAffected = await connection.ExecuteAsync(
            sql,
            new
            {
                req.Stock,
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