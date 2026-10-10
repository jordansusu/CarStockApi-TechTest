// Defines the contract for creating database connections

using System.Data;

namespace CarStockApi.Database;
public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}