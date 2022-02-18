using Dapper;
using MySql.Data.MySqlClient;
using SignalRApi.Data.Insterfazes;
using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRApi.Data.Repositorios
{
    public class RepoSalaCuenta: InterfaceSalaCuenta
    {
        private MySQLConfiguration _connectionString;
        public RepoSalaCuenta(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString._connectionString);
        }
        public async Task<bool> AsiganrSala(Sala_Cuenta salaCuenta)
        {
            var db = dbConnection();
            var sql = @"sp_insertar_sala_cuenta(@_id_cuenta,@_id_sala);";
            var result = await db.ExecuteAsync(sql, new { _id_sala=salaCuenta.id_sala,_id_cuenta=salaCuenta.id_cuenta});
            return result > 0;
        }
    }
}
