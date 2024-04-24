using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using IARecommendAPI.Data;
using IARecommendAPI.Mappers;
using IARecommendAPI.Modelos;
using IARecommendAPI.Repositorios.IRepositorios;
using IARecommendAPI.Repositorios;

var builder = WebApplication.CreateBuilder(args);


//Conexión a SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
}
);

//Soporte para autenticación con .NET Identity 
builder.Services.AddIdentity<Usuarios, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//Agregamos los repositorios
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IPeliculaRepositorio, PeliculaRepositorio>();

//Configuración de la autenticación
var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//Agregar los Automappers
builder.Services.AddAutoMapper(typeof(PaginaWebMapper));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description =
        "Autenticación JWT usando el esquema Bearer.\r\n\r\n" +
        "Ingresa la palabra 'Bearer' seguida de un [espacio] y después su token en el campo de abajo \r\n\r\n" +
        "Ejemplo: \"Bearer asdfasdfasdfasdf\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

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
