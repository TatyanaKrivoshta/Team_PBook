using Dapper;
using Npgsql;
using PBook_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task Add_Book(int person_id, int type_id, string number)
        {
            await Connection.OpenAsync();
            const string sql = """
                                INSERT INTO phonebook.table_book(person_id, type_id, number)
                                VALUES (@person_id, @type_id, @number)                               
                                """;
            await Connection.QuerySingleOrDefaultAsync<Book>(sql, new { person_id, type_id, number });
            Connection.Close();
        }


        public async Task Update_Book(int id,int person_id, int type_id, string number)
        {
            await Connection.OpenAsync();
            const string sql = """
                                UPDATE phonebook.table_book SET person_id=@person_id, type_id=@type_id, number=@number
                                WHERE id = @id;
                                """;
            await Connection.QuerySingleOrDefaultAsync<Book>(sql, new {id, person_id, type_id, number });
            Connection.Close();
        }

        public async Task Delete_Book(int id)
        {
            await Connection.OpenAsync();
            const string sql = """
                                DELETE FROM phonebook.table_book 
                                WHERE id = @id;
                                """;
            await Connection.QuerySingleOrDefaultAsync<Book>(sql, new { id });
            Connection.Close();
        }



    }
}
