using System.Data;
using Npgsql;
using Trench.Notification.Application.Contracts.Data;

namespace Trench.Notification.Persistence.Data;

internal sealed class SqlConnectionFactory(
    string connectionString)
    : ISqlConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        return connection;
    }
}