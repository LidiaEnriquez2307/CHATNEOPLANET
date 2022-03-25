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
    public class SalaController : ControllerBase
    {
        //inyectar el repositorio
        private readonly InterfaceSala _repoSala;
        public SalaController(InterfaceSala repoSala)
        {
            this._repoSala = repoSala;
        }
       [HttpPost]
        public async Task<IActionResult> insertar_sala([FromBody] Sala sala)
        {
            if (sala == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _repoSala.insertar_sala(sala);
            return Created("created", created);
        }
        [HttpPut("/borrar/{id_sala}")]
        public async Task<IActionResult> borrar_sala(int id_sala)
        {
            if (id_sala == 0)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repoSala.borrar_sala(id_sala);
            return NoContent();
        }
        [HttpPut("/nombre/{id_sala}/{nombre}")]
        public async Task<IActionResult> cambiar_nombre_sala(int id_sala, string nombre)
        {
            if (id_sala == 0)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repoSala.cambiar_nombre_sala(id_sala, nombre);
            return NoContent();
        }
    }
}
