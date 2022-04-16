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
    public class RepoCuentaSala : InterfaceCuentaSala
    {
        private MySQLConfiguration _connectionString;
        public RepoCuentaSala(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString._connectionString);
        }
        //CRUD
        public async Task<bool> vincular_cuenta_sala(Cuenta_Sala cuenta_sala)
        {
            var db = dbConnection();
            var sql = @"call sp_vincular_cuenta_sala(@_id_cuenta,@_id_sala,@_administrador);";
            var result = await db.ExecuteAsync(sql, new { _id_cuenta = cuenta_sala.id_cuenta, _id_sala = cuenta_sala.id_sala, _administrador = cuenta_sala.administrador });
            return result > 0;
        }
        public async Task<bool> desvincular_cuenta_sala(int id_sala_cuenta)
        {
            var db = dbConnection();
            var sql = @"call sp_desvincular_cuenta_sala(@_id_sala_cuenta);";
            var result = await db.ExecuteAsync(sql, new { _id_sala_cuenta = id_sala_cuenta});
            return result > 0;
        }

        public async Task<IEnumerable<Sala>> salas_de_una_cuenta(int id_cuenta)
        {
            var db = dbConnection();
            var sql = @"sp_salas_de_una_cuenta(@_id_cuenta)";
            return await db.QueryAsync<Sala>(sql, new { _id_cuenta = id_cuenta});
        }
        public async Task<IEnumerable<bool>> sincroniza_sala_cuenta_amigos(int id_cuenta)
        {
            var db = dbConnection();
            var sql = @"sp_sincronizar_salas_amigos(@_id_cuenta)";
            return await db.QueryAsync<bool>(sql, new { _id_cuenta = id_cuenta });
        }
    }
}
