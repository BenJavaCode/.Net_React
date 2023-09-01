using Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.Persistence;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// add midleware for Cors policy specified earlier
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

// using here means that this is cleaned up after use..
// scope is destroyed after it has run.
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    // seed data fromm seed.cs
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}
app.Run();
