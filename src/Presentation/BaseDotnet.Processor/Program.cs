using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BaseDotnet.Application;
using BaseDotnet.Infrastructure;
using BaseDotnet.Application.Interfaces;
using BaseDotnet.Processor.Services;
using BaseDotnet.Processor;

var builder = Host.CreateApplicationBuilder(args);

// Register dependencies
builder.Services.AddSingleton<ICurrentUserService, SystemCurrentUserService>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
