// Handle GET /api/cars requests

using CarStockApi.Auth;
using CarStockApi.Database;
using CarStockApi.Features.Cars.ListCars;
using CarStockApi.Models;
using Dapper;

namespace CarStockApi.Endpoints.ListCars;

public class ListCarsEndpoint : DealerEndpointWithoutRequest<List<ListCarsResponse>>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ListCarsEndpoint(IDbConnectionFactory database)
    {
        _connectionFactory = database;
    }
    public override void Configure()
    {
        base.Configure();
        Get("/api/cars");
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
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
            WHERE DealerId = @DealerId;
            """;

        var cars = await connection.QueryAsync<Car>(
            sql,
            new { DealerId }
            );

        var res = cars.Select(car => new ListCarsResponse
        {
            Id = car.Id,
            Brand = car.Brand,
            Model = car.Model,
            Year = car.Year,
            Stock = car.Stock
        }).ToList();

        await Send.OkAsync(res, ct);
    }

}