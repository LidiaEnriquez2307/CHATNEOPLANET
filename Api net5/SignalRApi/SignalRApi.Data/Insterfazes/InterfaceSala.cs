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
        Task<IEnumerable<String>> GetSalas(int id_cuenta);
        Task<bool> InsertSala(Sala sala);
    }
}
