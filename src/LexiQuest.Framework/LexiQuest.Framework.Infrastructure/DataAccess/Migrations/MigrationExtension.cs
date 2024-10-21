using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace LexiQuest.Framework.Infrastructure.DataAccess.Migrations;

public static class MigrationExtension
{
    public static NpgsqlDbContextOptionsBuilder UseMigrationTable(this NpgsqlDbContextOptionsBuilder x, string schema)
    {
        return x.MigrationsHistoryTable("__migrations", schema);
    }
}