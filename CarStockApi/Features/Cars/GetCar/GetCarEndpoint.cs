// Handles GET /api/cars/{id} requests

using CarStockApi.Auth;
using CarStockApi.Database;
using CarStockApi.Models;
using Dapper;

namespace CarStockApi.Features.Cars.GetCar;

public class GetCarEndpoint : DealerEndpointWithoutRequest<GetCarResponse>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetCarEndpoint(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public override void Configure()
    {
        base.Configure();
        Get("/api/cars/{id}");
    }

    public override async Task HandleAsync (CancellationToken ct)
    {
        // Get the Id from url
        var carId = Route<int>("id");
        using var connection = _connectionFactory.CreateConnection();
        const string sql = """
            SELECT
                id,
                DealerId,
                Brand,
                Model,
                Year,
                Stock
            FROM Cars
            WHERE Id = @CarId
                AND DealerId = @DealerId
            """;

        var car_info = await connection.QuerySingleOrDefaultAsync<Car>(
            sql,
            new { 
                    CarId = carId,
                    DealerId
                });

        if (car_info is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        var res = new GetCarResponse
        {
            Id = car_info.Id,
            Brand = car_info.Brand,
            Model = car_info.Model,
            Year = car_info.Year,
            Stock = car_info.Stock
        };

        await Send.OkAsync(res, ct);
    }
}