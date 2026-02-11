using CLTSalesSystem.API.Middleware;
using CLTSalesSystem.Application.Interfaces;
using CLTSalesSystem.Application.Services;
using CLTSalesSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar DbContext (Oracle) - Usando una cadena de conexión de ejemplo o obtenida de configuración
// NOTA: Para este ejemplo, asegúrese de tener "ConnectionStrings:DefaultConnection" en appsettings.json
builder.Services.AddDbContext<SalesDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection") ?? "User Id=myUser;Password=myPassword;Data Source=localhost:1521/XEPDB1;"));

// Inyección de Dependencias de Servicios de Aplicación
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IVentaService, VentaService>();
// builder.Services.AddValidatorsFromAssemblyContaining<ProductoDTOValidator>(); // Si se usara FluentValidation aquí

// Configuración de JWT
var jwtKey = builder.Configuration["Jwt:Key"] ?? "SecretKeyForTestingPurposesOnly12345";
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false, // Simplificado para prueba
        ValidateAudience = false // Simplificado para prueba
    };
});

var app = builder.Build();

// Middleware de manejo de errores global
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
