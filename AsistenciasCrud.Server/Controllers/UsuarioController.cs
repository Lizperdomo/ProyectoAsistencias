using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistenciasCrud.Server.Models;
using AsistenciasCrud.Shared;

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

        [HttpGet("Mostrar")]
        public async Task<IActionResult> Mostrar()
        {
            var usuarios = await _dbContext.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var usuario = await _dbContext.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado.");
            return Ok(usuario);
        }

        [HttpPost("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] UsuariosDTP usuarioDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo de usuario no válido.");

            var usuario = new Usuarios
            {
                Nombre = usuarioDto.Nombre,
                ApellidoP = usuarioDto.ApellidoP,
                ApellidoM = usuarioDto.ApellidoM,
                Correo = usuarioDto.Correo,
                Telefono = usuarioDto.Telefono
            };

            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
            return Ok(usuario.IdUsuario);
        }


        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] Usuarios usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo de usuario no válido.");

            var dbUsuario = await _dbContext.Usuarios.FindAsync(id);
            if (dbUsuario == null)
                return NotFound("Usuario no encontrado.");

            dbUsuario.Nombre = usuario.Nombre;
            dbUsuario.ApellidoP = usuario.ApellidoP;
            dbUsuario.ApellidoM = usuario.ApellidoM;
            dbUsuario.Correo = usuario.Correo;
            dbUsuario.Telefono = usuario.Telefono;

            _dbContext.Usuarios.Update(dbUsuario);
            await _dbContext.SaveChangesAsync();
            return Ok(dbUsuario.IdUsuario);
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuario = await _dbContext.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            _dbContext.Usuarios.Remove(usuario);
            await _dbContext.SaveChangesAsync();
            return Ok("Usuario eliminado.");
        }
    }
}