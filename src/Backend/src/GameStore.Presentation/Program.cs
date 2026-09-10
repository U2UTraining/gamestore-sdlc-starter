using GameStore.Application;
using GameStore.Infrastructure;
using GameStore.Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
await app.Services.EnsureDatabaseCreatedAsync();

app.MapApiEndpoints();


await app.RunAsync();
