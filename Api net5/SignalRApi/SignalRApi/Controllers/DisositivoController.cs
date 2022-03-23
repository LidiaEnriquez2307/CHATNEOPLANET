using Microsoft.AspNetCore.Mvc;
using SignalRApi.Data.Insterfazes;
using System.Threading.Tasks;
using SignalRApi.Modelos;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisositivoController : ControllerBase
    {
        //inyectar el repositorio
        private readonly InterfaceDispositivo _repoDispositivo;
        public DisositivoController(InterfaceDispositivo repoDisositivo)
        {
            this._repoDispositivo = repoDisositivo;
        }
        //
        [HttpGet]
        public async Task<IActionResult> getToken(int id_cuenta, int id_sala)
        {
            return Ok(await _repoDispositivo.mostrar_tokens(id_cuenta,id_sala));
        }
        [HttpGet("{token}")]
        public async Task<IActionResult> ExisteToken(string token)
        {
            return Ok(await _repoDispositivo.existe_token(token));
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
