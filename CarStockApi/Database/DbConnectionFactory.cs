using System.Data;
using Microsoft.Data.Sqlite;

namespace CarStockApi.Database;

public class DatabaseConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public DatabaseConnectionFactory()
    {
        _connectionString = "Data Source=Database/cars.db";
    }

    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(_connectionString);
    }
}