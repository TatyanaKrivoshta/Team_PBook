using PBook_DAL.Tables;
using PBook_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBook_BL2
{
    internal class Service2
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        TablePerson tablePerson;
        TablePhoneType tablePhoneType;
        TableBook tableBook;

        public Service2()
        {
            try
            {
                tablePerson = new TablePerson();
                tablePhoneType = new TablePhoneType();
                tableBook = new TableBook();
                _logger.Info("успешная инициализация таблиц классов из слоя DAL в слое BL");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Таблицы класоов в слое BL не созданы");
            }
        }

        //book
        public async Task<IEnumerable<Book>> GetAllBook_Async()
        { return await tableBook.GetAll_Async(); }

        public async Task<Book> GetBookById_Async(int id)
        { return await tableBook.GetById_Async(id); }

        public async Task AddBook_Async(int person_id, int type_id, string number)
        { await tableBook.Add_Book(person_id, type_id, number); }

        public async Task AddBook_Async(string first_name, string last_name, string patronymic, int type_id, string number)
        { await tableBook.Add_Book2(first_name, last_name, patronymic, type_id, number); }

        public async Task UpdateBook_Async(int id, int person_id, int type_id, string number)
        { await tableBook.Update_Book(id, person_id, type_id, number); }
        public async Task UpdateBook_Async(int id, string first_name, string last_name, string patronymic, int type_id, string number)
        { await tableBook.Update_Book2(id, first_name, last_name, patronymic, type_id, number); }

        public async Task DeleteBook_Async(int id)
        { await tableBook.Delete_Book(id); }


        //-----
        public async Task<IEnumerable<Person>> GetAllPerson_Async()
        { return await tablePerson.GetAll_Async(); }

        public async Task<Person> GetPersonById_Async(int id)
        { return await tablePerson.GetById_Async(id); }

        public async Task<IEnumerable<PhoneType>> GetAllPhoneType_Async()
        { return await tablePhoneType.GetAll_Async(); }

        public async Task<PhoneType> GetPhoneTypeById_Async(int id)
        { return await tablePhoneType.GetById_Async(id); }

        public async Task AddPerson(string first_name, string last_name, string patronymic)
        { await tablePerson.Add_Person(first_name, last_name, patronymic); }

        public async Task UpdatePepson_Async(int id, string first_name, string last_name, string patronymic)
        { await tablePerson.Update_Person(id, first_name, last_name, patronymic); }

        public async Task DeletePerson_Async(int id)
        { await tablePerson.Delete_Person(id); }

        public async Task<int> GetPersonIdByFullName(string first_name, string last_name, string patronymic)
        { return await tablePerson.GetIdByFullName(first_name, last_name, patronymic); }




    }
}
