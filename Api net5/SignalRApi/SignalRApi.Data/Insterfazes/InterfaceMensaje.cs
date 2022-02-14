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
        Task<IEnumerable<string>> GetMenajes(int id_sala);
        Task<bool> InsertMensaje(Mensaje mensaje);
        Task<bool> DeleteMensaje(int id_mensaje);
    }
}
