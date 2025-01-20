using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AsistenciasCrud.Server.Models;
using AsistenciasCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace AsistenciasCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {

        private readonly RegistroasistenciaContext _dbContext;

        public AsistenciaController(RegistroasistenciaContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Mostrar")]
        public async Task<IActionResult> Mostrar()
        {
            var responseApi = new ResponseAPI<List<Asistencia>>();
            var listaAsistencia = new List<Asistencia>();

            try
            {
                var asistencias = await _dbContext.Asistencias
                    .Include(a => a.Usuario)
                    .ToListAsync();

                foreach (var item in asistencias)
                {
                    listaAsistencia.Add(new Asistencia
                    {
                        IdAsistencia = item.IdAsistencia,
                        IdUsuario = item.IdUsuario,
                        HoraEntrada = item.HoraEntrada,
                        HoraSalida = item.HoraSalida,
                        Fecha = item.Fecha,
                        Usuario = new Usuario
                        {
                            IdUsuario = item.Usuario.IdUsuario,
                            Nombre = item.Usuario.Nombre,
                        }
                    });
                }

                responseApi.Correcto = true;
                responseApi.Valor = listaAsistencia;
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
            var responseApi = new ResponseAPI<Asistencia>();
            var Asistencia = new Asistencia();

            try
            {
                var dbAsistencia = await _dbContext.Asistencias.FirstOrDefaultAsync(x => x.IdAsistencia == id);

                if (dbAsistencia != null)
                {
                    Asistencia.IdAsistencia = dbAsistencia.IdAsistencia;
                    Asistencia.IdUsuario = dbAsistencia.IdUsuario;
                    Asistencia.HoraEntrada = dbAsistencia.HoraEntrada;
                    Asistencia.HoraSalida = dbAsistencia.HoraSalida;
                    Asistencia.Fecha = dbAsistencia.Fecha;

                    responseApi.Correcto = true;
                    responseApi.Valor = Asistencia;
                }
                else
                {
                    responseApi.Correcto = false;
                    responseApi.Mensaje = "Asistencia no encontrada";
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

        public async Task<IActionResult> Agregar(Asistencia asistencia)
        {
            var responseApi = new ResponseAPI<int>();
            var Asistencia = new Asistencia();

            try
            {
                var dbAsistencia = new Asistencia
                {
                    IdAsistencia = asistencia.IdAsistencia,
                    IdUsuario = asistencia.IdUsuario,
                    HoraEntrada = asistencia.HoraEntrada,
                    HoraSalida = asistencia.HoraSalida,
                    Fecha = asistencia.Fecha,
                };

                _dbContext.Asistencias.Add(dbAsistencia);
                await _dbContext.SaveChangesAsync();

                if (dbAsistencia.IdAsistencia != 0)
                {
                    responseApi.Correcto = true;
                    responseApi.Valor = dbAsistencia.IdAsistencia;
                }
                else
                {
                    responseApi.Correcto = false;
                    responseApi.Mensaje = "No guardado";
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

        public async Task<IActionResult> Editar(Asistencia asistencia, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbAsistencia = await _dbContext.Asistencias.FirstOrDefaultAsync(e => e.IdAsistencia == id);

                if (dbAsistencia != null)
                {
                    dbAsistencia.HoraEntrada = asistencia.HoraEntrada;
                    dbAsistencia.HoraSalida = asistencia.HoraSalida;
                    dbAsistencia.Fecha = asistencia.Fecha;

                    _dbContext.Asistencias.Update(dbAsistencia);
                    await _dbContext.SaveChangesAsync();

                    responseApi.Correcto = true;
                    responseApi.Valor = dbAsistencia.IdAsistencia;
                }
                else
                {
                    responseApi.Correcto = false;
                    responseApi.Mensaje = "Asistencia no encontrada";
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
                var dbAsistencia = await _dbContext.Asistencias.FirstOrDefaultAsync(e => e.IdAsistencia == id);

                if (dbAsistencia != null)
                {
                    _dbContext.Asistencias.Remove(dbAsistencia);
                    await _dbContext.SaveChangesAsync();

                    responseApi.Correcto = true;
                }
                else
                {
                    responseApi.Correcto = false;
                    responseApi.Mensaje = "Asistencia no encontrada";
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
