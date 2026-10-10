// Handles GET /api/cars/search request

using CarStockApi.Auth;
using CarStockApi.Database;
using CarStockApi.Models;
using Dapper;

namespace CarStockApi.Features.Cars.SearchCar;

public class SearchCarEndpoint : DealerEndpoint<SearchCarRequest, List<SearchCarResponse>>
{
    private readonly IDbConnectionFactory  _connectionFactory;
    public SearchCarEndpoint(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public override void Configure()
    {
        base.Configure();
        Get("/api/cars/search");
    }

    public override async Task HandleAsync(SearchCarRequest req, CancellationToken ct)
    {
        using var connection = _connectionFactory.CreateConnection();

        // Store the SQL conditions that will be added into the WHERE clause
        var conditions = new List<string>();
        // Stores the real values for the SQL parameters
        var parameters = new DynamicParameters();
        
        if (!string.IsNullOrWhiteSpace(req.Brand))
        {
            conditions.Add("Brand LIKE @Brand");
            parameters.Add("Brand", $"%{req.Brand}%");
        }

        if (!string.IsNullOrWhiteSpace(req.Model))
        {
            conditions.Add("Model LIKE @Model");
            parameters.Add("Model", $"%{req.Model}%");
        }

        parameters.Add("DealerId", DealerId);
        
        var sql = """
            SELECT
                Id,
                DealerId,
                Brand,
                Model,
                Year,
                Stock
            FROM Cars
            WHERE DealerId = @DealerId
            """;

        if (conditions.Count > 0)
        {
            sql += " AND " + string.Join(" AND ", conditions);
        }

        var cars = await connection.QueryAsync<Car>(
            sql,
            parameters
        );

        var res = cars.Select(c => new SearchCarResponse
        {
           Id = c.Id,
           Brand = c.Brand,
           Model = c.Model,
           Year = c.Year,
           Stock = c.Stock 
        }).ToList();

        await Send.OkAsync(res, ct);
    }

}