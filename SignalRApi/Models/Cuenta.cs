using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApi.Modelos
{
    public class Cuenta
    {
       public int id_cuenta { set; get; }
        public string correo { set; get; }
        public string contrasenia { set; get; }

    }
}
