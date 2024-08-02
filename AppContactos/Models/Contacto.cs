using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppContactos.Models
{
    public class Contacto
    {
        public int IdContacto { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
    }
}