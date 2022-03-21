using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRApi.Data.Insterfazes
{
    public interface InterfaceCuentaSala
    {
        Task<bool> vincular_cuenta_sala(Cuenta_Sala cuenta_sala);
        Task<bool> desvincular_cuenta_sala(int id_sala_cuenta);
        Task<IEnumerable<string>> salas_de_una_cuenta(int id_cuenta);//Se recupera solo los datos de la tabla SALA 
    }
}
