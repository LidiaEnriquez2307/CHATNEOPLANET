using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSignalR.Models
{
    public class Cuenta
    {
        public int id_cuenta { get; set; }
        public bool correo { get; set; }
        public bool contrasenia { get; set; }


    }
}
