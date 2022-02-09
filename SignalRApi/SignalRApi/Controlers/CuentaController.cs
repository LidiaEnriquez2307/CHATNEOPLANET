using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using Dapper;
using Data;

namespace SignalRApi.Controlers
{
    [Route("[controller]")]
    public class CuentaController : Controller
    {
                    private readonly InterfasCuenta _reposCuenta;
        public CuentaController(InterfasCuenta reposcuenta)
        {
            _reposCuenta = reposcuenta;
        }
        [HttpGet]
        public async Task<IActionResult> GetCuentas()
        {
            return Ok(await _reposCuenta.GetCuentas());
        }
        [HttpGet("{correo}")]
        public async Task<IActionResult> GetIdCuenta(string correo)
        {
            return Ok(await _reposCuenta.GetIdCuenta(correo));
        }
        
    }
}
