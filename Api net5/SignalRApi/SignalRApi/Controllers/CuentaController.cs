using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRApi.Data.Insterfazes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalRApi.Data;
using SignalRApi.Modelos;
using SignalRApi.NotificacionesPush;

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
        [HttpGet]
        public async Task<IActionResult> mostrar_cuentas()
        {
            FireBase fireBase = new FireBase();
            fireBase.NotificarSala(new Mensaje { id_cuenta=1,id_sala=1,mensaje="Hola"});
            return Ok(await _repoCuenta.mostrar_cuentas());
        }
        [HttpGet("{correo}")]
        public async Task<IActionResult> id_cuenta(string correo)
        {
            return Ok(await _repoCuenta.id_cuenta(correo));
        }
        [HttpPost]
        public async Task<IActionResult> insertar_cuenta([FromBody]Cuenta cuenta)
        {
            if (cuenta==null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _repoCuenta.insertar_cuenta(cuenta);
            return Created("created",created);
        }
    }
}
