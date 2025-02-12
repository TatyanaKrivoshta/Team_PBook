using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBook_BL;
using PBook_Model;

namespace Team_PBook_wpf.Model
{
    public class ClassTest
    {
        public Service service;
        public ObservableCollection<Person> persons= [];
        public ObservableCollection<Book> books=[];
        public ObservableCollection<PhoneType> phoneTypes=[];
        public ClassTest()
        {
            service = new Service();
            Test_function();
        }
        public async Task Test_function()
        {
            persons = new ObservableCollection<Person>( await service.GetAllPerson_Async());
            int c=persons.Count();
            books = new ObservableCollection<Book>(await service.GetAllBook_Async());
            int d=books.Count();
            phoneTypes = new ObservableCollection<PhoneType>(await service.GetAllPhoneType_Async());
            int e=phoneTypes.Count();
        }
    }
}
