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
        private readonly InterfaceSala _RepoSala;
        public SalaController(InterfaceSala RepoSala)
        {
            this._RepoSala = RepoSala;
        }
        [HttpGet("{id_cuenta}")]
        public async Task<IActionResult> GetSalas(int id_cuenta)
        {
            return Ok(await _RepoSala.GetSalas(id_cuenta));
        }
       [HttpPost]
        public async Task<IActionResult> InsertCuenta([FromBody] Sala sala)
        {
            if (sala == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _RepoSala.InsertSala(sala);
            return Created("created", created);
        }
    }
}
