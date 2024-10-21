using Autofac.Extensions.DependencyInjection;
using LexiQuest.WebApp;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder
   .ConfigLogger()
   .RegisterConnectionString()
   .RegisterDbContext()
   .RegisterModules()
   .RegisterMassTransit()
   .RegisterServices()
   .RegisterAuth()
   .RegisterWeb()
   .Build()
   .SetupWebApp()
   .Run();