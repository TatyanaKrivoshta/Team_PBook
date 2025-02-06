using PBook_DAL.Tables;
using PBook_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBook_BL
{
    public class Service
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        TablePerson tablePerson;
        TablePhoneType tablePhoneType;
        TableBook tableBook;
        public Service()
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

        public async Task<IEnumerable<Book>> GetAllBook_Async()
        { return await tableBook.GetAll_Async(); }

        public async Task<Book> GetBookById_Async(int id)
        { return await tableBook.GetById_Async(id); }

        public async Task<IEnumerable<Person>> GetAllPerson_Async()
        { return await tablePerson.GetAll_Async(); }

        public async Task<Person> GetPersonById_Async(int id)
        { return await tablePerson.GetById_Async(id); }

        public async Task<IEnumerable<PhoneType>> GetAllPhoneType_Async()
        { return await tablePhoneType.GetAll_Async(); }

        public async Task<PhoneType> GetPhoneTypeById_Async(int id)
        { return await tablePhoneType.GetById_Async(id); }

    }
}
