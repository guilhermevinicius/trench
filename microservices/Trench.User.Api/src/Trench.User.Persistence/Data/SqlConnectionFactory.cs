using System.Data;
using Npgsql;
using Trench.User.Application.Contracts.Data;

namespace Trench.User.Persistence.Data;

internal sealed class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        return connection;
    }
}
