using gestionDeTareas.Data;
using gestionDeTareas.Services;
using gestionDeTareas.Services.IServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar Puerto para Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS - IMPORTANTE
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

var app = builder.Build();

// Swagger habilitado siempre para que puedas probar en Render
app.UseSwagger();
app.UseSwaggerUI();

// Middleware
app.UseHttpsRedirection();

// Usar CORS antes de Authorization
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();