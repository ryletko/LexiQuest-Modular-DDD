using LexiQuest.WebApp.Client.Services;
using LexiQuest.WebApp.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddSingleton<IPersistingState, PersistingState>();
builder.Services.AddBlazorBootstrap();

builder.Services.AddHttpClient("WebApi", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebApi"));
builder.Services.AddTransient<IApiService, ApiService>();

await builder.Build().RunAsync();