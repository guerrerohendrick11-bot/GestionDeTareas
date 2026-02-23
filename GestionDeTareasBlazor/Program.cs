using GestionDeTareas.Blazor.Services;
using GestionDeTareasBlazor.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddScoped(sp =>
{
    var env = builder.Environment;

    if (env.IsDevelopment())
    {
        return new HttpClient
        {
            BaseAddress = new Uri("http://localhost:8080")
        };
    }
    else
    {
        return new HttpClient
        {
            BaseAddress = new Uri("https://gestiondetareas-2.onrender.com")
        };
    }
});

builder.Services.AddScoped<TareaApiService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();