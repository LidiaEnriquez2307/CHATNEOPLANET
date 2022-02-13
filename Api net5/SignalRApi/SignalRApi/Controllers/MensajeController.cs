using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRApi.Data.Insterfazes;
using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensajeController : ControllerBase
    {
        private readonly InterfaceMensaje _reposMensaje;
        public MensajeController(InterfaceMensaje MensajeRepos)
        {
            this._reposMensaje = MensajeRepos;
        }
        [HttpGet("{id_sala}")]
        public async Task<IActionResult> mostrar_mensajes(int id_sala)
        {
            return Ok(await _reposMensaje.mostrar_mensajes(id_sala));
        }
        [HttpGet]
        public async Task<IActionResult> buscar_mensaje(int id_cuenta, string mensaje)
        {
            return Ok(await _reposMensaje.buscar_mensaje(id_cuenta,mensaje));
        }
        [HttpPost]
        public async Task<IActionResult> insertar_mensaje([FromBody] Mensaje mensaje)
        {
            if (mensaje == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _reposMensaje.insertar_mensaje(mensaje);
            return Created("created", created);
        }
    }
}
