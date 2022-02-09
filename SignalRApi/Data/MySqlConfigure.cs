using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class MySqlConfigure
    {
        public MySqlConfigure(string connetionString)
        {
            ConnectionString = connetionString;
        }
        public string ConnectionString { get; set; }
    }
}
