using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRApi.Data.Insterfazes
{
    public interface InterfaceSalaCuenta
    {
        Task<bool> AsiganrSala(Sala_Cuenta salaCuenta);
    }
}
