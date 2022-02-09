using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRApi.Data.Insterfazes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalRApi.Data;
using SignalRApi.Modelos;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        //inyectar el repositorio
        private readonly InterfaceCuenta _RepoCuenta;
        public CuentaController(InterfaceCuenta RepoCuenta)
        {
            this._RepoCuenta = RepoCuenta;
        }
        [HttpGet]
        public async Task<IActionResult> GetCuentas()
        {
            return Ok(await _RepoCuenta.GetCuentas());
        }
        [HttpGet("{correo}")]
        public async Task<IActionResult> GetIdCuenta(string correo)
        {
            return Ok(await _RepoCuenta.GetIdCuenta(correo));
        }
        [HttpPost]
        public async Task<IActionResult> InsertCuenta([FromBody]Cuenta cuenta)
        {
            if (cuenta==null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _RepoCuenta.InsertCuenta(cuenta);
            return Created("created",created);
        }
    }
}
