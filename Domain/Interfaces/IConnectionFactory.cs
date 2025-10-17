using System.Data.Common;

namespace Domain.Interfaces;

public interface IConnectionFactory
{
    DbConnection CreateConnection();
}
