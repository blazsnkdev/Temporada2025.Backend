using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Temporada2025.Backend;
using Temporada2025.Backend.Data;
using Temporada2025.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

//variable de entorno
var frontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "http://localhost:5173";


//DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cn1")));

//Repositoy
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJugadorRepository,JugadorRepository>();
builder.Services.AddScoped<IEstadisticaRepository, EstadisticaRepository>();


//services
builder.Services.AddScoped<IJugadorService, JugadorService>();
builder.Services.AddScoped<IEstadisticaService, EstadisticaService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MiPoliticaCors", policy =>
    {
        policy.WithOrigins(frontendUrl)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//añadi esto para los cors
app.UseCors("MiPoliticaCors");



app.MapControllers();

app.Run();
