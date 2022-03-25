using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRApi.Data.Insterfazes
{
    public interface InterfaceMensaje
    {
        Task<bool> insertar_mensaje(Mensaje mensaje);
        Task<bool> mensaje_activo(int id_mensaje, bool activo);
        Task<bool> mensaje_leido(int id_mensaje, bool activo);
        Task<bool> vaciar_chat(int id_sala);
        Task<IEnumerable<Mensaje>> mostrar_mensajes(int id_sala);
        Task<IEnumerable<Mensaje>> buscar_mensaje(int id_cuenta, string mensaje);
    }
}
