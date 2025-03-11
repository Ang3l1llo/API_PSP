using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Services;


var builder = WebApplication.CreateBuilder(args);

// Configuracion MongoDB 
builder.Services.Configure<MongoDBSettings>(
builder.Configuration.GetSection("MongoDB"));


builder.Services.AddSingleton<PlayerService>();
builder.Services.AddControllers();

// Registra el DbContext usando la base de datos en memoria
builder.Services.AddDbContext<PlayerContext>(opt =>
    opt.UseInMemoryDatabase("PlayersDB"));


// Registra Swagger para documentación y pruebas
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:8080") // Permite solicitudes desde Godot
           .AllowAnyHeader()
           .AllowAnyMethod();
});*/

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
