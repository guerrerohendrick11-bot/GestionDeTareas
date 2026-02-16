using gestionDeTareas.Data;
using gestionDeTareas.Services;
using gestionDeTareas.Services.IServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ?? Controllers
builder.Services.AddControllers();

// ?? Swagger (NO OpenApi mínimo)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ?? DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// ?? Services
builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

var app = builder.Build();

// ?? Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ?? Middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// ?? Controllers
app.MapControllers();

// ?? Redirigir raíz a Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
