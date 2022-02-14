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
    public class SalaCuentaController : ControllerBase
    {
        //inyectar el repositorio
        private readonly InterfaceSalaCuenta _RepoSalaCuenta;
        public SalaCuentaController(InterfaceSalaCuenta RepoSalaCuneta)
        {
            this._RepoSalaCuenta = RepoSalaCuneta;
        }
        [HttpPost]
    public async Task<IActionResult> InsertCuenta([FromBody] Sala_Cuenta sala_cuenta)
    {
        if (sala_cuenta == null)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var created = await _RepoSalaCuenta.AsiganrSala(sala_cuenta);
        return Created("created", created);
    }
    }
}
