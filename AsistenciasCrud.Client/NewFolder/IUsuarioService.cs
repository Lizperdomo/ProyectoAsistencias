using AsistenciasCrud.Shared;

namespace AsistenciasCrud.Client.NewFolder
{
    public interface IUsuarioService
    {
        Task<List<Usuarios>> Mostrar();
        Task<Usuarios> Buscar(int id);
        Task<int> Guardar(Usuarios usuario);
        Task<int> Editar(int id, Usuarios usuario);
        Task<bool> Eliminar(int id);
    }
}
