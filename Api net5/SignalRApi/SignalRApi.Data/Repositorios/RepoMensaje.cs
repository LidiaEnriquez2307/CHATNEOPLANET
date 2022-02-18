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
    public class RepoMensaje : InterfaceMensaje
    {
        private MySQLConfiguration _connectionString;
        public RepoMensaje(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString._connectionString);
        }
        //CRUD
        public async Task<bool> DeleteMensaje(int id_mensaje)
        {
            var db = dbConnection();
            var sql = @"Delete * from Mensaje where id_mensaje = @_id_mensaje";
            var result = await db.ExecuteAsync(sql, new {_id_mensaje=id_mensaje });
            return result > 0;
        }

        public async Task<IEnumerable<String>> GetMenajes(int id_sala)
        {
            var db = dbConnection();
            var sql = @"sp_mostrar_mensajes(@_id_cuenta)";
            return await db.QueryAsync<String>(sql, new {_id_cuenta=id_sala});
        }

        public async Task<bool> InsertMensaje(Mensaje mensaje)
        {
            var db = dbConnection();
            var sql = @"sp_insertar_mensaje(@_id_cuenta,@_id_sala,@_mensaje,@_fecha)";
            var result = await db.ExecuteAsync(sql,new { _id_cuenta=mensaje.id_cueta, _id_sala=mensaje.id_sala, _Mensaje=mensaje.mensaje, _fecha=mensaje.fecha });
            return result > 0;
        }
    }
}
