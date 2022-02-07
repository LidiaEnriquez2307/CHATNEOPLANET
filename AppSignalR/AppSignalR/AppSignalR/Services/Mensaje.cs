using System;
using System.Collections.Generic;
using System.Text;

namespace AppSignalR.Services
{
    public class Mensaje
    {
        public string Message { get; set; }
        public int id_cuenta { get; set; }
        public int id_sala { get; set; }
    }
}
