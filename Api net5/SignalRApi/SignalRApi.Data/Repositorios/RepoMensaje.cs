using Dapper;
using MySql.Data.MySqlClient;
using SignalRApi.Data.Insterfazes;
using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var sql = @"call sp_insertar_mensaje(@_id_cuenta,@_id_sala,@_mensaje)";
            var result = await db.ExecuteAsync(sql, new { _id_cuenta = mensaje.id_cuenta, _id_sala = mensaje.id_sala, _mensaje = mensaje.mensaje });
            return result > 0;
        }

        public async Task<IEnumerable<Mensaje>> mostrar_mensajes(int id_sala)
        {
            var db = dbConnection();
            var sql = @"call sp_mostrar_mensajes(@_id_sala)";
            return await db.QueryAsync<Mensaje>(sql, new { _id_sala = id_sala });
        }
        public async Task<bool> mensaje_activo(int id_mensaje, bool activo)
        {
            var db = dbConnection();
            var sql = @"call sp_mensaje_activo(@_id_mensaje,@_activo)";
            var result = await db.ExecuteAsync(sql, new { _id_mensaje = id_mensaje, _activo = activo });
            return result > 0;
        }
        public async Task<bool> mensaje_leido(int id_mensaje, bool leido)
        {
            var db = dbConnection();
            var sql = @"call sp_mensaje_leido(@_id_mensaje,@_leido)";
            var result = await db.ExecuteAsync(sql, new { _id_mensaje = id_mensaje, _leido = leido });
            return result > 0;
        }
        public async Task<bool> vaciar_chat(int id_sala)
        {
            var db = dbConnection();
            var sql = @"call sp_vaciar_chat(@_id_sala)";
            var result = await db.ExecuteAsync(sql, new { _id_sala = id_sala });
            return result > 0;
        }
    }
}
