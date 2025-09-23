using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
});
builder.Services.AddCors();
builder.Services.AddScoped<ITokenService, TokenService>(); //Inyeccion de dependencias, Scoped: una instancia por solicitud

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins(
        "http://localhost:4200",
        "https://localhost:4200"
    ));

app.MapControllers();

app.Run();
