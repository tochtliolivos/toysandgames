using DBService;
using Microsoft.EntityFrameworkCore;
using Models.Contracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


// Add services to the container.
builder.Services.AddDbContext<ToysAndGamesContext>(options => 
    options.UseSqlServer(
        configuration.GetConnectionString("ToysAndGamesConnection")
        ));
builder.Services.AddAutoMapper(typeof(MapperConfig));
//TODO: Move this to the service layer and create an static method that add thems
builder.Services.AddScoped<IToysAndGamesService, ToysAndGamesService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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


// Configure the HTTP request pipeline.
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
