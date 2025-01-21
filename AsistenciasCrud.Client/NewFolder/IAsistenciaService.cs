using AsistenciasCrud.Shared;

namespace AsistenciasCrud.Client.NewFolder
{
    public interface IAsistenciaService
    {
        Task<List<Asistencias>> Mostrar();
        Task<Asistencias> Buscar(int id);
        Task<int> Guardar(Asistencias asistencia);
        Task<int> Editar(Asistencias asistencia);
        Task<bool> Eliminar(int id);
    }
}
