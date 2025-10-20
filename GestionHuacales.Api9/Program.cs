using GestionHuacales.Api.DAL;
using GestionHuacales.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var ConnectionString = builder.Configuration.GetConnectionString("sqlite");
builder.Services.AddDbContextFactory<Contexto>(options => options.UseSqlite(ConnectionString));
builder.Services.AddScoped<EntradasHuacalesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestionHuacales API V1");
        c.RoutePrefix = string.Empty;
    }); 
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestionHuacales API V1");
        c.RoutePrefix = string.Empty;
    });
}
    app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
