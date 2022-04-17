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
        [HttpGet("{id_sala}/{id_cuenta}")]
        public async Task<IActionResult> mostrar_mensajes(int id_sala, int id_cuenta)
        {
            return Ok(await _reposMensaje.mostrar_mensajes(id_sala,id_cuenta));
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
        [HttpPut("/activo/{id_mensaje}/{activo}")]
        public async Task<IActionResult> mensaje_activo(int id_mensaje, bool activo)
        {
            if (id_mensaje == 0)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _reposMensaje.mensaje_activo(id_mensaje,activo);
            return NoContent();
        }
        [HttpPut("/leido/{id_mensaje}/{leido}")]
        public async Task<IActionResult> mensaje_leido(int id_mensaje, bool leido)
        {
            if (id_mensaje == 0)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _reposMensaje.mensaje_leido(id_mensaje,leido);
            return NoContent();
        }
        [HttpPut("{id_sala}")]
        public async Task<IActionResult> vaciar_chat(int id_sala)
        {
            if (id_sala == 0)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _reposMensaje.vaciar_chat(id_sala);
            return NoContent();
        }
    }
}
