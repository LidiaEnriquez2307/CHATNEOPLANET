using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface InterfasCuenta
    {
       Task<IEnumerable<Cuenta>> GetCuentas();
       Task<Cuenta> GetIdCuenta(string _correo);
    }
}
