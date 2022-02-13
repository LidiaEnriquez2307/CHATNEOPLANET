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
    public class TipoSalaController : ControllerBase
    {
        //inyectar el repositorio
        private readonly InterfaceTipoSala _repoTipoSala;
        public TipoSalaController(InterfaceTipoSala repoTipoSala)
        {
            this._repoTipoSala = repoTipoSala;
        }
        [HttpPost]
        public async Task<IActionResult> insertar_tipo_sala([FromBody] TipoSala tipoSala)
        {
            if (tipoSala == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _repoTipoSala.insertar_tipo_sala(tipoSala);
            return Created("created", created);
        }

    }
}
