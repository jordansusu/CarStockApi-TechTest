// Handle POST /api/cars

using CarStockApi.Auth;
using CarStockApi.Database;
using Dapper;

namespace CarStockApi.Features.Cars.AddCar;

public class AddCarEndpoint : DealerEndpoint<AddCarRequest, AddCarResponse>
{
    private readonly IDbConnectionFactory _connectionFactory;
    public AddCarEndpoint(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public override void Configure()
    {
        base.Configure();
        Post("/api/cars");
    }

    public override async Task HandleAsync(AddCarRequest req, CancellationToken ct)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = """
            INSERT INTO Cars (DealerId, Brand, Model, Year, Stock)
            VALUES
                (@DealerId, @Brand, @Model, @Year, @Stock)
            RETURNING Id;
            """;

        var carId = await connection.ExecuteScalarAsync<int>(
            sql,
            new
            {
                DealerId,
                req.Brand,
                req.Model,
                req.Year,
                req.Stock
            }
        );

        var res = new AddCarResponse
        {
            Id = carId,
            Brand = req.Brand,
            Model = req.Model,
            Year = req.Year,
            Stock = req.Stock
        };

        await Send.CreatedAtAsync<AddCarEndpoint>(new { id = carId }, res, cancellation: ct);
    }
}