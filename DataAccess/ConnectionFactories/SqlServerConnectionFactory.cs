using System.Data.Common;
using Microsoft.Data.SqlClient;
using Domain.Interfaces;

namespace DataAccess.ConnectionFactories;

public class SqlServerConnectionFactory : IConnectionFactory
{

    private readonly string _connectionString;

    public SqlServerConnectionFactory(string connectionString)
    {
        _connectionString = connectionString; 
    }

    public DbConnection CreateConnection()
    {
        var connection = new SqlConnection(_connectionString);
        return connection;
    }
}
