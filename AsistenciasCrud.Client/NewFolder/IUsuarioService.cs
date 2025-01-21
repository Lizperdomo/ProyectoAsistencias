
using AsistenciasCrud.Shared;

namespace AsistenciasCrud.Client.NewFolder
{
    public interface IUsuarioService
    {
        Task<List<UsuariosDTO>> Mostrar();
        Task<UsuariosDTO> Buscar(int id);
        Task<int> Guardar(UsuariosDTO usuario);
        Task<int> Editar(int id, UsuariosDTO usuario);
        Task<bool> Eliminar(int id);
    }
}
