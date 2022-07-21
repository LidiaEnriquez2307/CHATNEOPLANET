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
    public class RepoTipoSala : InterfaceTipoSala
    {
        private MySQLConfiguration _connectionString;
        public RepoTipoSala(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString._connectionString);
        }
        //CRUD
        public async Task<bool> insertar_tipo_sala(TipoSala tipoSala)
        {
            var db = dbConnection();
            var sql = @"call sp_insertar_tipo_sala(@_id_tipo_sala,@_nombre)";
            var result = await db.ExecuteAsync(sql, new { _id_tipo_sala = tipoSala.id_tipo_sala, _nombre = tipoSala.nombre});
            return result > 0;
        } 
    }
}
