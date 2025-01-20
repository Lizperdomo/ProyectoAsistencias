using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenciasCrud.Shared
{
    public class ResponseAPI<T>
    {
        public bool Correcto { get; set; }

        public T? Valor { get; set; }

        public string? Mensaje { get; set; }
    }
}
