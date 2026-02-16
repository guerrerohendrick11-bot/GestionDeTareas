using GestionDeTareasBlazor.Models.Dto;

namespace GestionDeTareas.Blazor.Services
{
    public class TareaApiService
    {
        private readonly HttpClient _http;

        public TareaApiService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("Api");
        }

        public async Task<List<TareaCreateDto>> GetTareasAsync()
        {
            return await _http.GetFromJsonAsync<List<TareaCreateDto>>("api/tareas")
                   ?? new List<TareaCreateDto>();
        }

       
        public async Task<bool> CreateTareaAsync(TareaCreateDto nuevaTarea)
        {
            var response = await _http.PostAsJsonAsync("api/tareas", nuevaTarea);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
