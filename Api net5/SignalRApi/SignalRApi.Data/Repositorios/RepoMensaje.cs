using Dapper;
using MySql.Data.MySqlClient;
using SignalRApi.Data.Insterfazes;
using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
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
        public async Task<IEnumerable<Mensaje>> buscar_mensaje(int id_cuenta, string mensaje)
        {
            var db = dbConnection();
            var sql = @"call sp_buscar_mensaje(@_id_cuenta,@_mensaje)";
            return await db.QueryAsync<Mensaje>(sql, new { _id_cuenta = id_cuenta,_mensaje = mensaje });
        }
        public async Task<bool> insertar_mensaje(Mensaje mensaje)
        {
            var db = dbConnection();
            var sql = @"call sp_insertar_mensaje(@_id_cuenta,@_id_sala,@_mensaje,@_fecha)";
            var result = await db.ExecuteAsync(sql, new { _id_cuenta = mensaje.id_cuenta, _id_sala = mensaje.id_sala, _mensaje = mensaje.mensaje, _fecha = mensaje.fecha });
            return result > 0;
        }

        public async Task<IEnumerable<Mensaje>> mostrar_mensajes(int id_sala)
        {
            var db = dbConnection();
            var sql = @"call sp_mostrar_mensajes(@_id_sala)";
            return await db.QueryAsync<Mensaje>(sql, new { _id_sala = id_sala });
        }
    }
}
