using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Utils.EfCore;

public static class MigrationExtension
{
    public static NpgsqlDbContextOptionsBuilder UseMigrationTable(this NpgsqlDbContextOptionsBuilder x, string schema)
    {
        return x.MigrationsHistoryTable("__migrations", schema);
    }
}