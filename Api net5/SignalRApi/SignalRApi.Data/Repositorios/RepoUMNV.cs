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
    public class RepoUMNV : InterfaceUMNV
    {
        private MySQLConfiguration _connectionString;
        public RepoUMNV(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString._connectionString);
        }
        //CRUD
        public async Task<bool> insertar_umnv(UMNV umnv)
        {
            var db = dbConnection();
            var sql = @"call sp_insertar_umnv(@_id_cuenta,@_id_sala,@_id_mensaje)";
            var result = await db.ExecuteAsync(sql, new {_id_cuenta=umnv.id_cuenta,_id_sala=umnv.id_sala,_id_mensaje=umnv.id_mensaje});
            return result > 0;
        }
    }
}
