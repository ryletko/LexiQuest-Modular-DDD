using Autofac;
using LexiQuest.Framework.Application.DataAccess;
using LexiQuest.Framework.Infrastructure.DataAccess;
using LexiQuest.Framework.Infrastructure.DataAccess.Migrations;
using LexiQuest.Framework.Infrastructure.DataAccess.SqlConnection;
using LexiQuest.Framework.Module.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace LexiQuest.Framework.Module.DataAccess;

internal record DataAccessModuleParameters(string DatabaseConnectionString,
                                           ILoggerFactory LoggerFactory,
                                           ModuleContext ModuleContext);

internal class DataAccessModule<T>(DataAccessModuleParameters parameters) : Autofac.Module where T : BaseDbContext
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SqlConnectionFactory>()
               .As<ISqlConnectionFactory>()
               .WithParameter("connectionString", parameters.DatabaseConnectionString)
               .InstancePerLifetimeScope();

        builder.Register(c =>
                         {
                             var dbContextOptionsBuilder = new DbContextOptionsBuilder<T>();
                             dbContextOptionsBuilder.UseNpgsql(parameters.DatabaseConnectionString,
                                                               x => x.UseMigrationTable(parameters.ModuleContext.SchemaName)
                                                                     .ExecutionStrategy((e) => new NonRetryingExecutionStrategy(e.CurrentContext.Context))
                             );
                             var dbContext = (T) Activator.CreateInstance(typeof(T), dbContextOptionsBuilder.Options, parameters.LoggerFactory);

                             return dbContext;
                         })
               .AsSelf()
               .As<DbContext>()
               .As<T>()
               .As<BaseDbContext>()
               .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(parameters.ModuleContext.InfrastructureAssembly)
               .Where(type => type.Name.EndsWith("Repository"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope()
               .FindConstructorsWith(new AllConstructorFinder());

        builder.RegisterType<QueryContext>()
               .As<IQueryContext>()
               .InstancePerLifetimeScope();
    }
}