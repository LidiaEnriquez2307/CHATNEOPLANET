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
    public class UMNVController : ControllerBase
    {
        private readonly InterfaceUMNV _repoUMNV;
        public UMNVController(InterfaceUMNV repoUMNV)
        {
            this._repoUMNV = repoUMNV;
        }
        [HttpPost]
        public async Task<IActionResult> insertar_umnv([FromBody] UMNV umnv)
        {
            if (umnv == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _repoUMNV.insertar_umnv(umnv);
            return Created("created", created);
        }
    }
}
