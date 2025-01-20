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
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<Usuarios>>>("api/Usuarios/Mostrar");

            if (result.Correcto)
                return result.Valor;
            else
                throw new Exception(result.Mensaje);
        }
        public async Task<Usuarios> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<Usuarios>>($"api/Usuarios/Buscar/{id}");

            if (result.Correcto)
                return result.Valor;
            else
                throw new Exception(result.Mensaje);
        }
        public async Task<int> Guardar(Usuarios usuario)
        {
            var result = await _http.PostAsJsonAsync("api/Usuarios/Guardar", usuario);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            
            if (response.Correcto)
                return response.Valor;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> Editar(Usuarios usuario)
        {
            var result = await _http.PutAsJsonAsync($"api/Usuarios/Editar/{usuario.IdUsuario}", usuario);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response.Correcto)
                return response.Valor;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Usuarios/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response.Correcto)
                return response.Correcto;
            else
                throw new Exception(response.Mensaje);
        }

    }
}

