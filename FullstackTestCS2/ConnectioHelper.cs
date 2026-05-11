using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace FullstackTestCS2
{
    public abstract class ConnectionHelper
    {
        public static string GetConnectionString(IConfiguration config)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Port = uint.Parse(config["DbPort"]!),
                Database = config["DbName"],
                UserID = config["DbUser"],
                Password = config["DbPassword"]
            };

            return builder.ConnectionString;
        }
    }
}
