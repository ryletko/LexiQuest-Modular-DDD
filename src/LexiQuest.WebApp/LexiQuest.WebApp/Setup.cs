using System;
using System.Linq;
using System.Net.Http;
using Auth0.AspNetCore.Authentication;
using LexiQuest.Framework.Application.Messages.Context;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.Framework.Infrastructure.DataAccess.Migrations;
using LexiQuest.Framework.Infrastructure.EventBus;
using LexiQuest.WebApp.Client;
using LexiQuest.WebApp.Components;
using LexiQuest.WebApp.Data;
using LexiQuest.WebApp.EventBus;
using LexiQuest.WebApp.Hubs;
using LexiQuest.WebApp.Services;
using LexiQuest.WebApp.Shared.Services;
using MassTransit;
using MassTransit.Metadata;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using ILogger = Serilog.ILogger;

namespace LexiQuest.WebApp;

static class Setup
{
    private static ILogger _logger { get; set; }

    private static ILogger _webApiLogger { get; set; }

    private static ConnectionString _connectionString { get; set; }

    public class SetupBuilder()
    {
    }

    public static WebApplicationBuilder ConfigLogger(this WebApplicationBuilder builder)
    {
        _logger = new LoggerConfiguration()
                 .MinimumLevel.Error()
                 .MinimumLevel.Override("MassTransit.Mediator", LogEventLevel.Error)
                 .Enrich.FromLogContext()
                 .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{Module}] [{Context}] {Message:lj}{NewLine}{Exception}")
                 .WriteTo.File(new CompactJsonFormatter(), "logs/logs")
                 .CreateLogger();

        _webApiLogger = _logger.ForContext("Module", "API");
        _webApiLogger.Information("Logger configured");

        builder.Host.UseSerilog(_webApiLogger);

        return builder;
    }

    public static WebApplicationBuilder RegisterConnectionString(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration.AddEnvironmentVariables();
        _connectionString = ConnectionString.FromString(config.Build().GetConnectionString("LexiQuest") ?? throw new InvalidOperationException("Connection string 'AngryWordsDbContext' not found."));
        _webApiLogger.Information($"Connection string: {_connectionString}");
        builder.Services.AddSingleton(_connectionString);
        return builder;
    }

    public static WebApplicationBuilder RegisterApiService(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<PassAllCookiesHandler>();
        builder.Services.AddHttpClient("WebApi",
                                       (c, client) =>
                                       {
                                           var httpContextAccessor = c.GetRequiredService<IHttpContextAccessor>();
                                           var httpContext = httpContextAccessor.HttpContext;
                                           var baseAddress = $"https://{httpContext.Request.Host}{httpContext.Request.PathBase}";
                                           client.BaseAddress = new Uri(baseAddress);
                                       })
               .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                                                         {
                                                             // это для того чтобы когда предотвартить ситуацию, когда с пререндеринга натыкаемся на unathorized,
                                                             // он автоматически перевод на страницу логина и возвращается 200, хотя толжно быть 401
                                                             AllowAutoRedirect = false
                                                         })
               .AddHttpMessageHandler<PassAllCookiesHandler>();
        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebApi"));
        builder.Services.AddTransient<IApiService, ApiService>();
        return builder;
    }

    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        builder.RegisterApiService();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IPersistingState, PersistingState>(); // transient потому что для каждого компонента должен быть свой сохранятор стейта
        return builder;
    }

    public static WebApplicationBuilder RegisterDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<WebAppDbContext>(
            o => { o.UseNpgsql(_connectionString.Value, x => x.UseMigrationTable(WebAppDbContext.SchemaName)); });

        return builder;
    }

    public static WebApplicationBuilder RegisterMassTransit(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<MessageBrokerSettings>(builder.Configuration.GetSection("MessageBroker"));
        builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);
        builder.Services.AddScoped<IEventBus, WebAppMassTransitEventBus>();
        builder.Services.AddMassTransit(x =>
                                        {
                                            x.UsingRabbitMq((ctx, cfg) =>
                                                            {
                                                                var settings = ctx.GetRequiredService<MessageBrokerSettings>();
                                                                cfg.Host(new Uri(settings.Host),
                                                                         h =>
                                                                         {
                                                                             h.Username(settings.Username);
                                                                             h.Password(settings.Password);
                                                                         });

                                                                cfg.ConfigureEndpoints(ctx);
                                                                cfg.UseInMemoryOutbox(ctx);
                                                            });

                                            x.AddConfigureEndpointsCallback((context, name, cfg) =>
                                                                            {
                                                                                //if LiveServer {
                                                                                cfg.UseMessageRetry(r => r.Intervals(100)); //, 500, 1000, 5000, 10000));
                                                                                // }
                                                                                cfg.UseEntityFrameworkOutbox<WebAppDbContext>(context);
                                                                            });

                                            MessageCorrelation.UseCorrelationId<IContextedMessage>(c => c.MessageContext.CorrelationId);

                                            x.AddEntityFrameworkOutbox<WebAppDbContext>(c =>
                                                                                        {
                                                                                            c.UsePostgres();
                                                                                            c.UseBusOutbox();
                                                                                            c.DuplicateDetectionWindow = TimeSpan.FromMinutes(30);
                                                                                        });

                                            var consumerTypes = typeof(Setup).Assembly.GetTypes().Where(RegistrationMetadata.IsConsumerOrDefinition).ToArray();
                                            x.AddConsumers(consumerTypes);
                                        });

        return builder;
    }

    public static WebApplicationBuilder RegisterAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

        builder.Services
               .AddAuth0WebAppAuthentication(options =>
                                             {
                                                 options.Domain   = builder.Configuration["Auth0:Domain"];
                                                 options.ClientId = builder.Configuration["Auth0:ClientId"];
                                             });

        builder.Services.AddAuthorization();
        return builder;
    }

    public static WebApplicationBuilder RegisterWeb(this WebApplicationBuilder builder)
    {
        builder.Services
               .AddRazorComponents()
               .AddInteractiveServerComponents()
               .AddInteractiveWebAssemblyComponents();
        builder.Services.AddBlazorBootstrap();
        builder.Services.AddControllers();
        builder.Services.AddSignalR();
        builder.Services.AddResponseCompression(opts =>
                                                {
                                                    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                                                        ["application/octet-stream"]);
                                                });

        return builder;
    }

    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<ModulesHostedService>(
            c => new ModulesHostedService(c.GetRequiredService<ConnectionString>(),
                                          c.GetRequiredService<MessageBrokerSettings>(),
                                          _logger).Configure());
        return builder;
    }

    public static WebApplication SetupWebApp(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<WebAppDbContext>();
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }
        }

        app.UseResponseCompression();

        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapGet("/Account/Login",
                   async (HttpContext httpContext, string redirectUri = "/") =>
                   {
                       var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                                                     .WithRedirectUri(redirectUri)
                                                     .Build();

                       await httpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
                   });

        app.MapGet("/Account/Logout",
                   async (HttpContext httpContext, string redirectUri = "/") =>
                   {
                       var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                                                     .WithRedirectUri(redirectUri)
                                                     .Build();

                       await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
                       await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                   });

        app.MapRazorComponents<App>()
           .AddInteractiveServerRenderMode()
           .AddInteractiveWebAssemblyRenderMode()
           .AddAdditionalAssemblies(typeof(IWASM).Assembly);

        app.MapHub<StartNewGameStateHub>("/startgamestatehub");
        app.MapHub<ImportPuzzlesStateHub>("/importpuzzlesstatehub");
        app.MapHub<PuzzlesHub>("/puzzleshub");
        app.MapHub<GameHub>("/gamehub");
        app.MapHub<ErrorHub>("/errorshub");

        app.MapControllers();
        return app;
    }
}