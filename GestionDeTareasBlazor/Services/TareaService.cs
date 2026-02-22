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

       
        public async Task<List<TareaReadDto>> GetTareasAsync()
        {
            return await _http.GetFromJsonAsync<List<TareaReadDto>>("api/tareas")
                   ?? new List<TareaReadDto>();
        }

      
        public async Task<bool> CreateTareaAsync(TareaCreateDto nuevaTarea)
        {
            var response = await _http.PostAsJsonAsync("api/tareas", nuevaTarea);
            return response.IsSuccessStatusCode;
        }

        
        public async Task<bool> DeleteTareaAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/tareas/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}