using LexiQuest.Framework.Module.InternalProcessing;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.Framework.Module.DataAccess;

public static class Migrator
{
    public static void ApplyDbMigrations(CompositionRoot compositionRoot)
    {
        using (var scope = compositionRoot.BeginScope())
        {
            var dbContext = scope.GetDbContext();
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }
        }
    }
}