using Microsoft.AspNetCore.Mvc;
using SignalRApi.Data.Insterfazes;
using System.Threading.Tasks;
using SignalRApi.Modelos;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispositivoController : ControllerBase
    {
        //inyectar el repositorio
        private readonly InterfaceDispositivo _repoDispositivo;
        public DispositivoController(InterfaceDispositivo repoDisositivo)
        {
            this._repoDispositivo = repoDisositivo;
        }
        //
        [HttpGet("{id_cuenta}/{id_sala}")]
        public async Task<IActionResult> getToken(int id_cuenta, int id_sala)
        {
            return Ok(await _repoDispositivo.mostrar_tokens(id_cuenta,id_sala));
        }
        [HttpPost]
        public async Task<IActionResult> insertar_dispositivo([FromBody] Dispositivo dispositivo)
        {
            if (dispositivo == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _repoDispositivo.insertar_dispositivo(dispositivo);
            return Created("created", created);
        }
    }
}
