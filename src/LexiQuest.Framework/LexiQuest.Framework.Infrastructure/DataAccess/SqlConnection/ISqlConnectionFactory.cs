using System.Data;

namespace LexiQuest.Framework.Infrastructure.DataAccess.SqlConnection;

public interface ISqlConnectionFactory
{
    IDbConnection GetOpenConnection();

    IDbConnection CreateNewConnection();

    string GetConnectionString();
}