﻿using Dapper;
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
    public class TableBook : BaseTable
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

            /*   const string sql = """
                              SELECT id, first_name, last_name, patronymic, type,number
                              FROM view_book
                              WHERE id = @id
                              """;*/
            const string sql = """
                               SELECT phonebook.table_book.id,
                               first_name, last_name, patronymic,
                               type,
                               number
                                  FROM phonebook.table_book
                                  JOIN phonebook.table_persons
                                      ON phonebook.table_book.person_id=phonebook.table_persons.id
                                  JOIN phonebook.table_phonetype
                                      ON phonebook.table_book.type_id=phonebook.table_phonetype.id
                             WHERE phonebook.table_book.id=@id
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
        public async Task Add_Book2(string first_name, string last_name, string patronymic, int type_id, string number)
        {
            await Connection.OpenAsync();
            const string sql = """
                                CALL procedure_create_book3(@first_name, @last_name, @patronymic, @type_id, @number);                           
                                """;

            using var command = new NpgsqlCommand(sql, Connection);
            command.Parameters.AddWithValue("@first_nameP", first_name);
            command.Parameters.AddWithValue("@last_nameP", last_name);
            command.Parameters.AddWithValue("@patronymicP", patronymic);
            command.Parameters.AddWithValue("@type_idP", type_id);
            command.Parameters.AddWithValue("@numberP", number);

            await Connection.QuerySingleOrDefaultAsync<Book>(sql, new { first_name, last_name, patronymic, type_id, number });

            Connection.Close();
        }

        public async Task Update_Book(int id, int person_id, int type_id, string number)
        {
            await Connection.OpenAsync();
            const string sql = """
                                UPDATE phonebook.table_book SET person_id=@person_id, type_id=@type_id, number=@number
                                WHERE id = @id;
                                """;
            await Connection.QuerySingleOrDefaultAsync<Book>(sql, new { id, person_id, type_id, number });
            Connection.Close();
        }

        public async Task Update_Book2(int id, string first_name, string last_name, string patronymic, int type_id, string number)
        {
            await Connection.OpenAsync();
            const string sql = """
                                CALL procedure_update_book("id,@first_name, @last_name, @patronymic, @type_id, @number);                           
                                """;

            using var command = new NpgsqlCommand(sql, Connection);
            command.Parameters.AddWithValue("@book_idP", id);
            command.Parameters.AddWithValue("@first_nameP", first_name);
            command.Parameters.AddWithValue("@last_nameP", last_name);
            command.Parameters.AddWithValue("@patronymicP", patronymic);
            command.Parameters.AddWithValue("@type_idP", type_id);
            command.Parameters.AddWithValue("@number", number);

            await command.ExecuteNonQueryAsync();

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
