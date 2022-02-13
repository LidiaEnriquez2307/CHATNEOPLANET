using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRApi.Data.Insterfazes
{
    public interface InterfaceSala
    {
        Task<bool> insertar_sala(Sala sala);
        Task<bool> cambiar_nombre_sala(int id_sala, string nombre);
        Task<bool> borrar_sala(int id_sala);
    }
}
