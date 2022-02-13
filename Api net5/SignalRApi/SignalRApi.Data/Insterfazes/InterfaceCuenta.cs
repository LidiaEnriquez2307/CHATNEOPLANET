using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRApi.Data.Insterfazes
{
    public interface InterfaceCuenta
    {
        Task<bool> insertar_cuenta(Cuenta cuenta);
        Task<IEnumerable<Cuenta>> mostrar_cuentas();
        Task<IEnumerable<Cuenta>> id_cuenta(string correo);
    }
}
