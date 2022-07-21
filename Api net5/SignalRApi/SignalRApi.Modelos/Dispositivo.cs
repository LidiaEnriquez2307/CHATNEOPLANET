using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRApi.Modelos
{
    public class Dispositivo
    {
        public string codigoUnico { get; set; }
        public int id_dispositivo{ get; set; }
        public int id_cuenta { get; set; }
        public string token { get; set; }
    }
}
