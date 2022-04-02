using MySql.Data.MySqlClient;
using SignalRApi.Data.Insterfazes;
using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
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
            var sql = @"call sp_insertar_dispositivo(@_codigoUnico,@_id_cuenta,@_token)";
            var result = await db.ExecuteAsync(sql, new { _codigoUnico=dispositivo.codigoUnico, _id_cuenta = dispositivo.id_cuenta, _token=dispositivo.token });
            return result > 0;
        }

        public async Task<IEnumerable<string>> mostrar_tokens(int id_cuenta, int id_sala)
        {
            var db = dbConection();
            var sql = @"call sp_mostrar_tokens(@_id_cuenta,@_id_sala)";
            return await db.QueryAsync<string>(sql, new { _id_cuenta=id_cuenta, _id_sala=id_sala });
        }
        public async Task<IEnumerable<string>> existe_token(string token)
        {
            var db = dbConection();
            var sql = @"call sp_existe_token(@_token)";
            return await db.QueryAsync<string>(sql, new { _token = token});
        }
    }
}
