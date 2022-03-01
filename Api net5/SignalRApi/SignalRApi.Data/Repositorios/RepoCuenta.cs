using MySql.Data.MySqlClient;
using SignalRApi.Data.Insterfazes;
using SignalRApi.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace SignalRApi.Data.Repositorios
{
    public class RepoCuenta : InterfaceCuenta
    {
        private MySQLConfiguration _connectionString;
        public RepoCuenta(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        //conexion
        protected MySqlConnection dbConection()
        {
            return new MySqlConnection(_connectionString._connectionString);
        }
        //CRUD
        public async Task<IEnumerable<Cuenta>> id_cuenta(string correo)
        {
            var db = dbConection();
            var sql = @"call sp_id_cuenta(@_correo)";
            return await db.QueryAsync<Cuenta>(sql, new { _correo = correo });
        }

        public async Task<bool> insertar_cuenta(Cuenta cuenta)
        {
            var db = dbConection();
            var sql = @"call sp_insertar_cuenta(@_correo,@_contrasenia)";
            var result = await db.ExecuteAsync(sql, new { _correo = cuenta.correo, _contrasenia = cuenta.contrasenia });
            return result > 0;
        }

        public async Task<IEnumerable<Cuenta>> mostrar_cuentas()
        {
            var db = dbConection();
            var sql = @"call sp_mostrar_cuentas()";
            return await db.QueryAsync<Cuenta>(sql, new { });
        }
        public Task<IEnumerable<Cuenta>> mostrar_cuenta(int id_cuenta)
        {
            var db = dbConection();
            var sql = @"call sp_mostrar_cuenta(@_id_cuenta)";
            return db.QueryAsync<Cuenta>(sql, new { _id_cuenta =id_cuenta});
        }
    }
}
