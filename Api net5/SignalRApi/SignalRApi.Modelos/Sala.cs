﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRApi.Modelos
{
    public class Sala
    {
        public int id_sala { get; set; }
        public int id_tipo_sala { get; set; } //1=privado; 2= publica
        public string nombre { get; set; }
        public DateTime fecha { get; set; }
        public bool activo { get; set; }
    }
}