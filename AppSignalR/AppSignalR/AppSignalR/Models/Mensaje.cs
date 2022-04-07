using System;
using System.Collections.Generic;
using System.Text;

namespace AppSignalR.Models
{
    public class Mensaje
    {
        public int id_mensaje { get; set; }
        public int id_cuenta { get; set; }
        public int id_sala { get; set; }
        public string mensaje { get; set; }
        public DateTime fecha { get; set; }
        public bool activo { get; set; }
        public bool leido { get; set; }
        public bool remitente { get; set; }
    }
}
