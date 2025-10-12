using System.Data.Common;

namespace DataAccess.ConnectionFactories;

public interface IConnectionFactory
{
    DbConnection CreateConnection();
}
