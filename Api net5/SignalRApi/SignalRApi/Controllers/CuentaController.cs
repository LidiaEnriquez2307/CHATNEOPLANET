using Microsoft.AspNetCore.Mvc;
using SignalRApi.Data.Insterfazes;
using System.Threading.Tasks;
using SignalRApi.Modelos;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        //inyectar el repositorio
        private readonly InterfaceCuenta _repoCuenta;
        public CuentaController(InterfaceCuenta repoCuenta)
        {
            this._repoCuenta = repoCuenta;
        }

        [HttpGet("{correo}")]
        public async Task<IActionResult> id_cuenta(string correo)
        {
            return Ok(await _repoCuenta.id_cuenta(correo));
        }
        [HttpGet("/autor/{id_cuenta}")]
        public async Task<IActionResult> nombre_cuenta(int id_cuenta)
        {
            return Ok(await _repoCuenta.TraerAutor(id_cuenta));
        }
    }
}
