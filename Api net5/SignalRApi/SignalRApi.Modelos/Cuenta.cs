using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRApi.Modelos
{
    public class Cuenta
    {
        public int id_cuenta { get; set; }
        public string correo { get; set; }
        public string contrasenia { get; set; }
    }
}
