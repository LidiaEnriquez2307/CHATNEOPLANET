using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRApi.Modelos
{
    public class Cuenta_Sala
    {
        public int id_sala_cuenta { get; set; }
        public int id_cuenta { get; set; }
        public int id_sala { get; set; }
        public DateTime fecha { get; set; }
        public bool administrador { get; set; }
        public bool activo { get; set; }
    }
}
