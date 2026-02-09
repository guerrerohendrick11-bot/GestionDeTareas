using GestionDeTareasBlazor.Models;

namespace GestionDeTareas.Blazor.Services
{
    public class TareaApiService
    {
        private readonly HttpClient _http;

        public TareaApiService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("Api");
        }

        public async Task<List<TareasDto>> GetTareasAsync()
        {
            return await _http.GetFromJsonAsync<List<TareasDto>>("api/tareas")
                   ?? new List<TareasDto>();
        }
    }
}
