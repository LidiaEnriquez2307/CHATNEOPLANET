using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRApi.Modelos
{
    public class UMNV
    {
        public int id_umnv { get; set; }
        public int id_cuenta { get; set; }
        public int id_sala { get; set; }

        public int id_mensaje { get; set; }
        public DateTime fecha{ get; set; }
    }
}
