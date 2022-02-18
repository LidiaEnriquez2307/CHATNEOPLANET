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
        private readonly InterfaceMensaje _MensajeRepos;
        public MensajeController(InterfaceMensaje MensajeRepos)
        {
            this._MensajeRepos = MensajeRepos;
        }
        [HttpGet("{id_sala}")]
        public async Task<IActionResult> GetMensajesSala(int id_sala)
        {
            return Ok(await _MensajeRepos.GetMenajes(id_sala));
        }
        [HttpPost]
        public async Task<IActionResult> InsertCuenta([FromBody] Mensaje mensaje)
        {
            if (mensaje == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _MensajeRepos.InsertMensaje(mensaje);
            return Created("created", created);
        }
    }
}
