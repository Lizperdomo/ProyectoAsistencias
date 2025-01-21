using AsistenciasCrud.Shared;
using System.Net.Http.Json;

namespace AsistenciasCrud.Client.NewFolder
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _http;

        public UsuarioService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Usuarios>> Mostrar()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<Usuarios>>>("api/Usuario/Mostrar");

            if (result.Correcto)
                return result.Valor;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<Usuarios> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<Usuarios>>($"api/Usuario/Buscar/{id}");

            if (result.Correcto)
                return result.Valor;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> Guardar(Usuarios usuario)
        {
            var result = await _http.PostAsJsonAsync("api/Usuario/Agregar", usuario);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response.Correcto)
                return response.Valor;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> Editar(int id, Usuarios usuario)
        {
            var result = await _http.PutAsJsonAsync($"api/Usuario/Editar/{id}", usuario);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response.Correcto)
                return response.Valor;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Usuario/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response.Correcto)
                return true;
            else
                throw new Exception(response.Mensaje);
        }
    }
}
