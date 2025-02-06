using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBook_DAL.Tables
{
    public abstract class BaseTable
    {
        protected readonly Npgsql.NpgsqlConnection Connection;
        string configPath = "dbconfig.json";
        public DBConfig dbConfig { get; set; }
        //комментарий
        
        protected BaseTable()
        {
            dbConfig = new DBConfig();
            
            var config = DBConfig.Load(configPath);

            if (config is null) throw new DbConfigException();

            Connection = new Npgsql.NpgsqlConnection(config.ConnectionString);

            //Включение сопоставления имён с нижним подчёркиванием
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

       // public object DbConfig { get; private set; }
    }
}
