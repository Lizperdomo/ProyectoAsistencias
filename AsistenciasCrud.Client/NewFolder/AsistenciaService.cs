﻿using AsistenciasCrud.Shared;
using System.Net.Http.Json;

namespace AsistenciasCrud.Client.NewFolder
{
    public class AsistenciaService : IAsistenciaService
    {
        private readonly HttpClient _http;

        public AsistenciaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Asistencias>> Mostrar()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<Asistencias>>>("api/Asistencia/Mostrar");

            if (result.Correcto)
                return result.Valor;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<Asistencias> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<Asistencias>>($"api/Asistencia/Buscar/{id}");

            if (result.Correcto)
                return result.Valor;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> Guardar(Asistencias asistencia)
        {
            var result = await _http.PostAsJsonAsync("api/Asistencia/Agregar", asistencia);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response.Correcto)
                return response.Valor;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> Editar(Asistencias asistencia)
        {
            var result = await _http.PutAsJsonAsync($"api/Asistencia/Editar/{asistencia.IdAsistencia}", asistencia);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response.Correcto)
                return response.Valor;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Asistencia/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response.Correcto)
                return response.Correcto;
            else
                throw new Exception(response.Mensaje);
        }
    }
}
