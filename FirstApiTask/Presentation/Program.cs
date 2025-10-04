using FirstApiTask.Infrastructure.Configuration;
using FirstApiTask.Infrastructure.Data;
using FirstApiTask.Presentation.Middleware;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDI(builder.Configuration);

var app = builder.Build();

app.UseRouting();

app.UseAppBuilder(builder.Environment);

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<StorageDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
