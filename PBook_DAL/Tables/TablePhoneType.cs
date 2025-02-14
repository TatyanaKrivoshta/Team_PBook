using Dapper;
using Npgsql;
using PBook_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBook_DAL.Tables
{
    public class TablePhoneType :BaseTable
    {

        public async Task< IEnumerable<PhoneType>>? GetAll_Async()
        {
            try
            {
                await Connection.OpenAsync();

                const string sql = """
                                   SELECT id, type
                                   FROM phonebook.table_phonetype   
                                   """;

                var result = await Connection.QueryAsync<PhoneType>(sql);

                Connection.Close();
                return result;
            }
            catch (NpgsqlException e)
            {
                throw new GetDataFromTable(nameof(TablePhoneType), e);
            }
        }
        public async Task<PhoneType?> GetById_Async(int id)
        {
            await Connection.OpenAsync();

            const string sql = """
                           SELECT id, type
                           FROM phonebook.table_phonetype                           
                           WHERE id = @id
                           """;
            var type = await Connection.QuerySingleOrDefaultAsync<PhoneType>(sql, new { id });
            Connection.Close();
            return type;
        }

    }
}
