using System;
using System.Collections.Generic;
using System.Text;

namespace AppSignalR.Models
{
    public class Cuenta
    {
        public int? id_cuenta { get; set; }
        public object correo { get; set; }
        public object contrasenia { get; set; }
    }
}
