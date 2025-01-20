using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciasCrud.Shared
{
    public class Asistencias
    {
        public int IdAsistencia { get; set; }

        public int IdUsuario { get; set; }

        public TimeOnly HoraEntrada { get; set; }

        public TimeOnly HoraSalida { get; set; }

        public DateOnly Fecha { get; set; }

        public Usuarios? Usuario { get; set; }
    }
}
