using System.Data;

namespace Trench.User.Application.Contracts.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
