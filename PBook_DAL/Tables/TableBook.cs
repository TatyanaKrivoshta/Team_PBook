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
    public class TableBook :BaseTable
    {
        public async Task<IEnumerable<Book>?> GetAll_Async()
        {
            try
            {
                await Connection.OpenAsync();

                const string sql = """
                                   SELECT id, first_name, last_name, patronymic, type,number
                                   FROM view_book;                       
                                   """;

                var result = await Connection.QueryAsync<Book>(sql);

                Connection.Close();
                return result;
            }
            catch (NpgsqlException e)
            {
                throw new GetDataFromTable(nameof(TableBook), e);
            }
        }
        public async Task<Book?> GetById_Async(int id)
        {
            await Connection.OpenAsync();

            const string sql = """
                           SELECT id, first_name, last_name, patronymic, type,number
                           FROM view_book
                           WHERE id = @id
                           """;
            var book = await Connection.QuerySingleOrDefaultAsync<Book>(sql, new { id });

            Connection.Close();
            return book;
        }
    }
}
