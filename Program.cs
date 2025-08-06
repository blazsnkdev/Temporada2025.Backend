using Microsoft.EntityFrameworkCore;
using Temporada2025.Backend;
using Temporada2025.Backend.Data;
using Temporada2025.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

//DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cn1")));

//Repositoy
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJugadorRepository,JugadorRepository>();


//services
builder.Services.AddScoped<IJugadorService, JugadorService>();



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
