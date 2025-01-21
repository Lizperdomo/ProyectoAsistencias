﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AsistenciasCrud.Shared
{
    public partial class Usuarios
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string ApellidoP { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string ApellidoM { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El campo {0} debe tener exactamente 10 dígitos numéricos")]
        public string Telefono { get; set; } = null!;
    }
}
