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
    public class RepoSala : InterfaceSala
    {
        private MySQLConfiguration _connectionString;
        public RepoSala(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString._connectionString);
        }
        public async Task<IEnumerable<string>> GetSalas(int id_cuenta)
        {
            var db = dbConnection();
            var sql = @"sp_mostrar_salas(@_id_cuenta);";
           var result = await db.QueryAsync<string>(sql, new { _id_cuenta = id_cuenta });
            return result;
        }

        public async Task<bool> InsertSala(Sala sala)
        {
            var db = dbConnection();
            var sql = @"sp_insertar_sala(@_id_tipo_sala,@_Nombre);";
            var result = await db.ExecuteAsync(sql, new { _id_tipo_sala= sala.id_tipo_sala,_Nombre=sala.nombre });
            return result > 0;
        }
    }
}
