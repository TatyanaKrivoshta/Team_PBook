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
        public async Task Add_Person(string first_name, string last_name, string patronymic)
        {
            await Connection.OpenAsync();
            const string sql = """
                                INSERT INTO phonebook.table_persons(first_name, last_name, patronymic)
                                VALUES (@first_name, @last_name, @patronymic)
                                """;
            await Connection.QuerySingleOrDefaultAsync<Person>(sql, new { first_name, last_name, patronymic });
            Connection.Close();
        }
       

        public async Task Update_Person(int id, string first_name, string last_name, string patronymic)
        {
            await Connection.OpenAsync();
            const string sql = """
                                UPDATE phonebook.table_persons
                                SET first_name = @first_name, last_name= @last_name, patronymic= @patronymic
                                WHERE id = @id;
                                """;
            await Connection.QuerySingleOrDefaultAsync<Person>(sql, new {id, first_name, last_name, patronymic });
            Connection.Close();
        }

        public async Task Delete_Person(int id)
        {
            await Connection.OpenAsync();
            const string sql = """
                                DELETE  FROM phonebook.table_persons 
                                WHERE id = @id;
                                """;
            await Connection.QuerySingleOrDefaultAsync<Person>(sql, new { id });
            Connection.Close();
        }

        public async Task<int> GetIdByFullName(string first_name, string last_name, string patronymic)
        {
            await Connection.OpenAsync();

            const string sql = """
                               SELECT id
                               FROM phonebook.table_persons
                               WHERE first_name = @first_name  AND last_name = @last_name AND patronymic = @patronymic;
                               """;
            var person_id = await Connection.QuerySingleOrDefaultAsync<int>(sql, new { first_name, last_name, patronymic });

            Connection.Close();
            return person_id;
        }





    }
}
