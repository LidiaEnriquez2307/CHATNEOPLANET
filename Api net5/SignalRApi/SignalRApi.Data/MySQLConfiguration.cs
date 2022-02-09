using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Dapper;
using SignalRApi.Modelos;

namespace SignalRApi.Data
{
    public class MySQLConfiguration
    {
        public string _connectionString { set; get; }
        public MySQLConfiguration(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
