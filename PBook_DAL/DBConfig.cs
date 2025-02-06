using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PBook_DAL
{
    public class DBConfig
    {

        [JsonPropertyName("server")]
        public string Server { get; set; }

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("database")]
        public string Database { get; set; }

        [JsonPropertyName("user")]
        public string User { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("schema")]
        public string Schema { get; set; }

        public string ConnectionString => $"Server={Server};Port={Port};Database={Database};User Id={User};Password={Password};SearchPath={Schema};";

        public string configPathFromDBConfig = "dbconfig.json";

        //public  DBConfig? Load(string configPath = "dbconfig.json")
             public static DBConfig? Load(string configPath)
        {
           
            string? json = null;
            try
            {
                json = File.ReadAllText(configPath);
            }
            catch (Exception e)
            {
                throw new FileConfigException(configPath, e);
            }

            try
            {
                return JsonSerializer.Deserialize<DBConfig>(json);
            }
            catch (Exception e)
            {
                throw new DbConfigException(e);
            }
        }


    }
}
