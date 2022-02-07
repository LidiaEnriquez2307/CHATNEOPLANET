using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using Dapper;

namespace SignalRApi.Controlers
{
    [Route("[controller]")]
    public class CuentaController : Controller
    {
        private string cadena = @"Server=localhost; Database=CHAT; Uid=root;";
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Modelos.Cuenta> lista = null;
            using (var db= new MySqlConnection(cadena))
            
            {
                var sql = "select * from cuenta;";
                lista = db.Query<Modelos.Cuenta>(sql);
            }

            return Ok(lista);
        }
    }
}
