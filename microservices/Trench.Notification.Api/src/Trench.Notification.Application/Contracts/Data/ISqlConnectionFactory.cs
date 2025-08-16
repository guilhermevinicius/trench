using System.Data;

namespace Trench.Notification.Application.Contracts.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
