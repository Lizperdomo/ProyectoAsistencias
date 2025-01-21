using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AsistenciasCrud.Server.Models;
using AsistenciasCrud.Shared;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

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
            var responseApi = new ResponseAPI<List<dynamic>>(); 
            var listaAsistencia = new List<dynamic>();

            try
            {
                var asistencias = await _dbContext.Asistencias
                    .Include(a => a.Usuario)
                    .ToListAsync();

                foreach (var item in asistencias)
                {
                    listaAsistencia.Add(new
                    {
                        item.IdAsistencia,
                        item.IdUsuario,
                        HoraEntrada = item.HoraEntrada.ToString("HH:mm"),  
                        HoraSalida = item.HoraSalida.ToString("HH:mm"),    
                        Fecha = item.Fecha.ToString("yyyy-MM-dd"),         
                        Usuario = new
                        {
                            item.Usuario.IdUsuario,
                            item.Usuario.Nombre
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
            var responseApi = new ResponseAPI<dynamic>();  
            dynamic asistenciaResponse = new ExpandoObject(); 

            try
            {
                var dbAsistencia = await _dbContext.Asistencias
                    .Include(a => a.Usuario)
                    .FirstOrDefaultAsync(x => x.IdAsistencia == id);

                if (dbAsistencia != null)
                {
                    asistenciaResponse.IdAsistencia = dbAsistencia.IdAsistencia;
                    asistenciaResponse.IdUsuario = dbAsistencia.IdUsuario;
                    asistenciaResponse.HoraEntrada = dbAsistencia.HoraEntrada.ToString("HH:mm");
                    asistenciaResponse.HoraSalida = dbAsistencia.HoraSalida.ToString("HH:mm");
                    asistenciaResponse.Fecha = dbAsistencia.Fecha.ToString("yyyy-MM-dd");
                    asistenciaResponse.Usuario = new
                    {
                        dbAsistencia.Usuario.IdUsuario,
                        dbAsistencia.Usuario.Nombre
                    };

                    responseApi.Correcto = true;
                    responseApi.Valor = asistenciaResponse;
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
