using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRApi.Data.Insterfazes
{
    public interface InterfaceDispositivo
    {
        Task<bool> insertar_dispositivo(Dispositivo dispositivo);
        Task<IEnumerable<string>> mostrar_tokens(Mensaje mensaje);
    }
}
