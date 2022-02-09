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
        public int id_cueta { get; set; }
        public int id_sala { get; set; }
        public string mensaje { get; set; }
        public string fecha { get; set; }
    }
}
