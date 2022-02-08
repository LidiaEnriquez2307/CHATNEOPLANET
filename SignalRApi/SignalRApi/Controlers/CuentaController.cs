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
            var f = 0;
            IEnumerable<Modelos.Cuenta> lista = null;
            //IEnumerable<Modelos.Cuenta> lista = null;
            using (var db= new MySqlConnection(cadena))
            {
                var sql = "call sp_id_cuenta('usuario1@1')";
                lista = db.Query<Modelos.Cuenta>(sql);
            }
                foreach(var w in lista)
            {
                f = w.id_cuenta;
            }
            return Ok(f);
        }
    }
}
