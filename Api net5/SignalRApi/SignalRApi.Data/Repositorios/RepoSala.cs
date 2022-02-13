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
        //CRUD
        public async Task<bool> borrar_sala(int id_sala)
        {
            var db = dbConnection();
            var sql = @"call sp_borrar_sala(@_id_sala)";
            var result = await db.ExecuteAsync(sql, new { _id_sala = id_sala});
            return result > 0;
        }

        public async Task<bool> cambiar_nombre_sala(int id_sala, string nombre)
        {
            var db = dbConnection();
            var sql = @"call sp_cambiar_nombre_sala(@_id_sala,@_nombre)";
            var result = await db.ExecuteAsync(sql, new {_id_sala=id_sala,_nombre=nombre});
            return result > 0;
        }

        public async Task<bool> insertar_sala(Sala sala)
        {
            var db = dbConnection();
            var sql = @"call sp_insertar_sala(@_id_tipo_sala,@_nombre, @_fecha);";
            var result = await db.ExecuteAsync(sql, new { _id_tipo_sala = sala.id_tipo_sala, _nombre = sala.nombre, _fecha=sala.fecha });
            return result > 0;
        }
    }
}
