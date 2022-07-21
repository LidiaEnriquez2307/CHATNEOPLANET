using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRApi.Modelos
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
    }
}
