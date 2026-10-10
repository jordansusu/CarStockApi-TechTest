using System.Data;
using Microsoft.Data.Sqlite;

namespace CarStockApi.Database;

public class DatabaseConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public DatabaseConnectionFactory(IWebHostEnvironment environment)
    {
        // _connectionString = "Data Source=Database/cars.db";
        var databasePath = Path.Combine(
            environment.ContentRootPath,
            "Database",
            "cars.db"
        );

        _connectionString = $"Data Source={databasePath}";
    }

    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(_connectionString);
    }
}