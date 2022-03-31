using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSignalR.Models
{
    public class Cuenta
    {
        public int id_cuenta { get; set; }
        public string correo { get; set; }
        public string contrasenia { get; set; }


    }
}
