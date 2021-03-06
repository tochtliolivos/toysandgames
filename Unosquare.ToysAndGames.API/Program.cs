using Microsoft.EntityFrameworkCore;
using Unosquare.ToysAndGames.DBService;
using Unosquare.ToysAndGames.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddProductServiceDependencies(configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using var scope = app.Services.CreateScope();

using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
    .SetMinimumLevel(LogLevel.Trace)
    .AddConsole());
ILogger logger = loggerFactory.CreateLogger<Program>();

try
{    
    var db = scope.ServiceProvider.GetService<ToysAndGamesContext>();

    //HACK: for integration test we don't apply database migration
    if (db.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
        db.Database.Migrate();
}
catch (Exception e)
{
    logger.LogError("Error with seed information: " + e.Message);
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
