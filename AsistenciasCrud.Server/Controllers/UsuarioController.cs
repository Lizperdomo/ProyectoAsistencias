using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AsistenciasCrud.Server.Models;
using AsistenciasCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace AsistenciasCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly RegistroasistenciaContext _dbContext;

        public UsuarioController(RegistroasistenciaContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Mostrar")]

        public async Task<IActionResult> Mostrar()
        {
            var responseApi = new ResponseAPI<List<Usuario>>();
            var listaUsuario = new List<Usuario>();

            try
            {
                foreach (var item in await _dbContext.Usuarios.ToListAsync())
                {
                    listaUsuario.Add(new Usuario
                    {
                        IdUsuario = item.IdUsuario,
                        Nombre = item.Nombre,
                        ApellidoP = item.ApellidoP,
                        ApellidoM = item.ApellidoM,
                        Correo = item.Correo,
                        Telefono = item.Telefono,
                    });
                }

                responseApi.Correcto = true;
                responseApi.Valor = listaUsuario;

            }
            catch (Exception ex)
            {
                responseApi.Correcto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{id}")]

        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<Usuario>();
            var Usuario = new Usuario();

            try
            {
                var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);

                if (dbUsuario != null)
                {
                    Usuario.IdUsuario = dbUsuario.IdUsuario;
                    Usuario.Nombre = dbUsuario.Nombre;
                    Usuario.ApellidoP = dbUsuario.ApellidoP;
                    Usuario.ApellidoM = dbUsuario.ApellidoM;
                    Usuario.Correo = dbUsuario.Correo;
                    Usuario.Telefono = dbUsuario.Telefono;

                    responseApi.Correcto = true;
                    responseApi.Valor = Usuario;
                }
                else
                {
                    responseApi.Correcto = false;
                    responseApi.Mensaje = "Usuario no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.Correcto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Agregar")]

        public async Task<IActionResult> Agregar(Usuario usuario)
        {
            var responseApi = new ResponseAPI<int>();
            var Usuario = new Usuario();

            try
            {
                var dbUsuario = new Usuario
                {
                    IdUsuario = usuario.IdUsuario,
                    ApellidoP = usuario.ApellidoP,
                    ApellidoM = usuario.ApellidoM,
                    Correo = usuario.Correo,
                    Telefono = usuario.Telefono,
                };

                _dbContext.Usuarios.Add(dbUsuario);
                await _dbContext.SaveChangesAsync();

                if (dbUsuario.IdUsuario != 0)
                {
                    responseApi.Correcto = true;
                    responseApi.Valor = dbUsuario.IdUsuario;
                }
                else
                {
                    responseApi.Correcto = false;
                    responseApi.Mensaje = "Usuario no guardado";
                }
            }
            catch (Exception ex)
            {
                responseApi.Correcto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPut]
        [Route("Editar/{id}")]

        public async Task<IActionResult> Editar(Usuario usuario, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(e => e.IdUsuario == id);

                if (dbUsuario != null)
                {
                    dbUsuario.Nombre = usuario.Nombre;
                    dbUsuario.ApellidoP = usuario.ApellidoP;
                    dbUsuario.ApellidoM = usuario.ApellidoM;
                    dbUsuario.Correo = usuario.Correo;
                    dbUsuario.Telefono = usuario.Telefono;

                    _dbContext.Usuarios.Update(dbUsuario);
                    await _dbContext.SaveChangesAsync();

                    responseApi.Correcto = true;
                    responseApi.Valor = dbUsuario.IdUsuario;
                }
                else
                {
                    responseApi.Correcto = false;
                    responseApi.Mensaje = "Usuario no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.Correcto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]

        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(e => e.IdUsuario == id);

                if (dbUsuario != null)
                {
                    _dbContext.Usuarios.Remove(dbUsuario);
                    await _dbContext.SaveChangesAsync();

                    responseApi.Correcto = true;
                }
                else
                {
                    responseApi.Correcto = false;
                    responseApi.Mensaje = "Usuario no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.Correcto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }
    }
}
