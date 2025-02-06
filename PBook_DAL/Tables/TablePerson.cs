using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBook_Model;
using Dapper;

namespace PBook_DAL.Tables
{
    public class TablePerson : BaseTable
    {

        public async Task<IEnumerable<Person>>? GetAll_Async()
        {
            try
            {
               await Connection.OpenAsync();

                const string sql = """
                                   SELECT id,
                                          first_name, last_name, patronymic
                                   FROM phonebook.table_persons                            
                                   """;

                var result = await Connection.QueryAsync<Person>(sql);

                Connection.Close();
                return result;
            }
            catch (NpgsqlException e)
            {
                throw new GetDataFromTable(nameof(TablePerson), e);
            }
        }
        public async Task <Person?> GetById_Async(int id)
        {
            await Connection.OpenAsync();

            const string sql = """
                           SELECT id, first_name, last_name, patronymic
                           FROM phonebook.table_persons
                           WHERE id = @id
                           """;
            var person = await Connection.QuerySingleOrDefaultAsync<Person>(sql, new { id });

            Connection.Close();
            return person;
        }
    }
}
