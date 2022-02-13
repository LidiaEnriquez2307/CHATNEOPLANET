using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRApi.Data.Insterfazes
{
    public interface InterfaceUMNV
    {
        Task<bool> insertar_umnv(UMNV umnv);
    }
}
