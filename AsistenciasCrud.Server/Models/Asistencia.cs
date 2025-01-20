using System;
using System.Collections.Generic;

namespace AsistenciasCrud.Server.Models;

public partial class Asistencia
{
    public int IdAsistencia { get; set; }

    public int IdUsuario { get; set; }

    public TimeOnly HoraEntrada { get; set; }

    public TimeOnly HoraSalida { get; set; }

    public DateOnly Fecha { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    public Usuario UsuarioObj { get; internal set; }
}
