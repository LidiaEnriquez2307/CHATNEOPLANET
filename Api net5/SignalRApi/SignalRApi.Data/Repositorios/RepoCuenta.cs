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
        public async Task<IEnumerable<Cuenta>> GetCuentas()
        {
            var db = dbConection();
            var sql = @"sp_mostrar_cuentas()";
            return await db.QueryAsync<Cuenta>(sql,new { });
        }

        public async Task<Cuenta> GetIdCuenta(string correo)
        {
            var db = dbConection();
            var sql = @"sp_id_cuenta(@_correo)";
            return await db.QueryFirstOrDefaultAsync<Cuenta>(sql, new {_correo=correo});
        }

        public async Task<bool> InsertCuenta(Cuenta cuenta)
        {
            var db = dbConection();
            var sql = @"sp_insertar_cuenta(@_correo,@_contrasenia)";
            var result = await db.ExecuteAsync(sql, new { _correo = cuenta.correo, _contrasenia= cuenta.contrasenia });
            return result > 0;
        }
    }
}
