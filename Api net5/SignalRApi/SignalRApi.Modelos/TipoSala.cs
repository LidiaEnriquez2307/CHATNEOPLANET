using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRApi.Modelos
{
    public class TipoSala
    {
        public int id_tipo_sala { get; set; }
        public string nombre { get; set; }
        public DateTime fecha { get; set; }
        public bool activo { get; set; }
    }
}
