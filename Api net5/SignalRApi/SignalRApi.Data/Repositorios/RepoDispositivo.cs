using MySql.Data.MySqlClient;
using SignalRApi.Data.Insterfazes;
using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SignalRApi.Data.Repositorios
{
    public class RepoDispositivo : InterfaceDispositivo
    {
        private MySQLConfiguration _connectionString;
        public RepoDispositivo(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        //conexion
        protected MySqlConnection dbConection()
        {
            return new MySqlConnection(_connectionString._connectionString);
        }
        //CRUD
        public async Task<bool> insertar_dispositivo(Dispositivo dispositivo)
        {
            var db = dbConection();
            var sql = @"call sp_insertar_dispositivo(@_id_cuenta,@_token)";
            var result = await db.ExecuteAsync(sql, new { _id_cuenta=dispositivo.id_cuenta, _token=dispositivo.token });
            return result > 0;
        }

        public async Task<IEnumerable<string>> mostrar_tokens(Mensaje mensaje)
        {
            var db = dbConection();
            var sql = @"call sp_mostrar_tokens(@_id_cuenta, @_id_sala)";
            return await db.QueryAsync<string>(sql, new { _id_cuenta=mensaje.id_cuenta, _id_sala=mensaje.id_sala });
        }
    }
}
