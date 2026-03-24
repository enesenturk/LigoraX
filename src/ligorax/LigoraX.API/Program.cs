using AspectCore.Extensions.DependencyInjection;
using LigoraX.API;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());

builder.BuildApplication();

builder.Services.ConfigureDynamicProxy();

var app = builder.Build();

app.ConfigureApplication();

app.RunApplication();