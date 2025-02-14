using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBook_Client_DAL;
using PBook_Model;

namespace Team_PBook_wpf.Model
{
    public class ClassTest
    {
        public ClientDAL clientDal;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public ObservableCollection<Person> persons= [];
        public ObservableCollection<Book> books=[];
        public ObservableCollection<PhoneType> phoneTypes=[];
        public ClassTest()
        {
            clientDal = new ClientDAL();
            try
            {
                Test_function();
                Logger.Info("Коллекции БД загрузились с сервера");
            }
            catch (Exception e)
            {
                Logger.Fatal(e, "Коллекции базы данных НЕ ЗАГРУЗИЛИСЬ с сервера");
                throw;
            }
            
        }
        public async Task Test_function()
        {
            persons = new ObservableCollection<Person>( await clientDal.Dal_GetAllPerson_Async());
            int c=persons.Count();
            books = new ObservableCollection<Book>(await clientDal.Dal_GetAllBooks_Async());
            int d=books.Count();
            phoneTypes = new ObservableCollection<PhoneType>(await clientDal.Dal_GetAllPhoneType_Async());
            int e=phoneTypes.Count();
        }
    }
}
