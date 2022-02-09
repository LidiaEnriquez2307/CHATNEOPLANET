using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Dapper;

namespace SignalRApi.Modelos
{
    public class ReposCuenta:InterfasCuenta
    {
        private MySqlConfigure _connetionString;
       public ReposCuenta(MySqlConfigure connectionString)
        {
            _connetionString = connectionString;

        }
        public async Task<IEnumerable<Cuenta>> GetCuentas()
        {
            var db = dbConnection();
            var sql = @"call sp_mostrar_cuentas()";
            return await db.QueryAsync<Cuenta>(sql, new {});
        }
        public async Task<Cuenta> GetIdCuenta(string correo)
        {
            var db = dbConnection();
            var sql = @"call sp_Id_cuenta(@_correo)";
            return await db.QueryFirstOrDefaultAsync<Cuenta>(sql,new { _correo = correo });
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connetionString.ConnectionString);
        }

    }
}
