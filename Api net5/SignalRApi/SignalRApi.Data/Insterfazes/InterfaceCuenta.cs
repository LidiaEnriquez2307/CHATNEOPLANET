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
        Task<IEnumerable<Cuenta>> GetCuentas();
        Task<Cuenta> GetIdCuenta(string correo);
        Task<bool> InsertCuenta(Cuenta cuenta);
    }
}
