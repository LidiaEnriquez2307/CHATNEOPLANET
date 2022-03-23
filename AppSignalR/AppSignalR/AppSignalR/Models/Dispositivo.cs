using System;
using System.Collections.Generic;
using System.Text;

namespace AppSignalR.Models
{
    public class Dispositivo
    {
        public int id_dispositivo { get; set; }
        public int id_cuenta { get; set; }
        public string token { get; set; }
    }
}
