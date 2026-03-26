using AspectCore.Extensions.DependencyInjection;
using byLiGG.API;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());

builder.BuildApplication();

builder.Services.ConfigureDynamicProxy();

var app = builder.Build();

app.ConfigureApplication();

app.RunApplication();