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
    public class CuentaSalaController : ControllerBase
    {
        //inyectar el repositorio
        private readonly InterfaceCuentaSala _repoSalaCuenta;
        public CuentaSalaController(InterfaceCuentaSala repoSalaCuneta)
        {
            this._repoSalaCuenta = repoSalaCuneta;
        }
        //CRUD
        [HttpGet("{id_cuenta}")]
        public async Task<IActionResult> mostrar_salas(int id_cuenta)
        {
            return Ok(await _repoSalaCuenta.salas_de_una_cuenta(id_cuenta));
        }
        [HttpGet("sincronizar_con_amigos/{id_cuenta}")]
        public async Task<IActionResult> sincronizar_con_amigos(int id_cuenta)
        {
            return Ok(await _repoSalaCuenta.sincroniza_sala_cuenta_amigos(id_cuenta));
        }
        [HttpPost]
        public async Task<IActionResult> vincular_cuenta_sala([FromBody] Cuenta_Sala sala_cuenta)
        {
            if (sala_cuenta == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _repoSalaCuenta.vincular_cuenta_sala(sala_cuenta);
            return Created("created", created);
        }

        [HttpPut("{id_cuenta_sala}")]
        public async Task<IActionResult> desvincular_cuenta_sala(int id_cuenta_sala)
        {
            if (id_cuenta_sala == 0)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repoSalaCuenta.desvincular_cuenta_sala(id_cuenta_sala);
            return NoContent();
        }
    }
}
