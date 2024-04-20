using IARecommendAPI.Data;
using IARecommendAPI.Mappers;
using IARecommendAPI.Repositorios;
using IARecommendAPI.Repositorios.IRepositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configuramos a conexion a sql server
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
});

// Agregarmos los repositorios primera interfaz y luego repositorio
builder.Services.AddScoped<IPeliculaRepositorio, PeliculaRepositorio>();

// agregar automapper
builder.Services.AddAutoMapper(typeof(PeliculasMappers));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
