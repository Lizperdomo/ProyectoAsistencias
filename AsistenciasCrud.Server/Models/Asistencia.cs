using System.ComponentModel.DataAnnotations.Schema;

namespace AsistenciasCrud.Server.Models
{
    public partial class Asistencia
    {
        public int IdAsistencia { get; set; }

        [Column("idUsuario")]
        public int IdUsuario { get; set; }

        public TimeOnly HoraEntrada { get; set; }
        public TimeOnly HoraSalida { get; set; }
        public DateOnly Fecha { get; set; }

        public virtual Usuarios Usuario { get; set; }
    }
}
