using Microsoft.Extensions.Logging;
using PBook_BL;
using PBook_Model;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using NLog;
using NLog.Targets;

namespace PBook_Client_DAL
{
    public class ClientDAL
    {
        private static NLog.Logger logger2 = NLog.LogManager.GetCurrentClassLogger();

        private static readonly HttpClient Client = new();
        public Service service;

        public ObservableCollection<Book> books;
        public ObservableCollection<Person> persons; // ������� ��������� ������
        public ObservableCollection<PhoneType> phoneTypes;

        public ClientDAL()
        {
           
            service = new Service();
            try
            {
                Test_function();
                logger2.Info("� ������ Client_DAL ��������� ����� ������� ������������ ��������� �� ���� ��");
            }
            catch (Exception ex) 
            {
                logger2.Info("� ������ Client_DAL �� ��������� ����� ������� ������������ ��������� �� ���� ��");
            }
            try
            {
                Test_function2();
                logger2.Info("� ������ Client_DAL ��������� ����� ������� ������������ ��������� ����� �������");
            }
            catch(Exception ex)
            {
                logger2.Info("� ������ Client_DAL �� ��������� ����� ������� ������������ ��������� ����� �������");
            }
        }

        public async Task Test_function() //BL
        {
            try
            {
                persons = new ObservableCollection<Person>(await service.GetAllPerson_Async());
                int c = persons.Count();
                logger2.Info($"��������� Person � ���� BL �������, ���������� ��������� = {c} ");
            }
            catch(Exception ex)
            {
                logger2.Fatal(ex, "��������� Person � ���� BL �� �������");
            }
            try
            {
                books = new ObservableCollection<Book>(await service.GetAllBook_Async());
                int d = books.Count();
                logger2.Info($"��������� Book � ���� BL �������, ���������� ��������� = {d} ");
            }
            catch (Exception ex)
            {
                logger2.Fatal(ex, "��������� Book � ���� BL �� �������");
            }
            try
            {
                phoneTypes = new ObservableCollection<PhoneType>(await service.GetAllPhoneType_Async());
                int e = phoneTypes.Count();
                logger2.Info($"��������� phoneType � ���� BL �������, ���������� ��������� = {e} ");
            }
            catch (Exception ex)
            {
                logger2.Fatal(ex, "��������� phoneType � ���� BL �� �������");
            }

            
        }
        public async Task Test_function2()
        {
            try
            {
                persons = new ObservableCollection<Person>(await Dal_GetAllPerson_Async());
                int c = persons.Count();
                logger2.Info($"��������� Person � ���� Client_DAL �������, ���������� ��������� = {c} ");
            }
            catch (Exception ex)
            {
                logger2.Fatal(ex, "��������� Person � ���� Client_DAL �� �������");
            }
            try 
            {
                books = new ObservableCollection<Book>(await Dal_GetAllBooks_Async());
                int d = books.Count();
                logger2.Info($"��������� Book � ���� Client_DAL �������, ���������� ��������� = {d} ");
            }
            catch (Exception ex)
            {
                logger2.Fatal(ex, "��������� Book � ���� Client_DAL �� �������");
            }
            try
            {
                phoneTypes = new ObservableCollection<PhoneType>(await Dal_GetAllPhoneType_Async());
                int e = phoneTypes.Count();
                logger2.Info($"��������� phoneType � ���� Client_DAL �������, ���������� ��������� = {e} ");
            }
            catch (Exception ex)
            {
                logger2.Fatal(ex, "��������� phoneType � ���� Client_DAL �� �������");
            }

           
        }

        public async Task<IEnumerable<Book>> Dal_GetAllBooks_Async() =>
            await Client.GetFromJsonAsync<IEnumerable<Book>>
            (new Uri($"http://localhost:5182/books/"));

        public async Task<Book> Dal_GetBookById(int id) =>
            await Client.GetFromJsonAsync<Book>
            (new Uri($"http://localhost:5182/books/{id}"));

        public async Task Dal_AddBook(string first_name, string last_name, string patronymic, int type_id, string number) =>
            await Client.PostAsJsonAsync(new Uri($"http://localhost:5182/book/{first_name},{last_name},{patronymic},{type_id},{number}"),
                (first_name, last_name, patronymic, type_id, number));

        public async Task Dal_DeleteBook(int id) =>  await Client.DeleteAsync(new Uri($"{id}"));

        public async Task<IEnumerable<Person>> Dal_GetAllPerson_Async() =>
            await Client.GetFromJsonAsync<IEnumerable<Person>>
            (new Uri($"http://localhost:5182/persons/"));        
        
        public async Task<IEnumerable<Person>> Dal2_GetAllPerson_Async() => await Client.GetFromJsonAsync<IEnumerable<Person>>
            (new Uri($"http://localhost:5080/persons/"));

        public async Task<Person> Dal_GetPersonById_Async(int id) =>
            await Client.GetFromJsonAsync<Person>
            (new Uri($"http://localhost:5182/person/{id}"));

        public async Task<IEnumerable<PhoneType>> Dal_GetAllPhoneType_Async() =>
            await Client.GetFromJsonAsync<IEnumerable<PhoneType>>
            (new Uri($"http://localhost:5182/phoneTypes/"));

        public async Task<PhoneType> Dal_GetPhoneTypeById_Async(int id) =>
            await Client.GetFromJsonAsync<PhoneType>
            (new Uri($"http://localhost:5182/phoneType/{id}"));

        public async Task Dal_AddPerson(string first_name, string last_name, string patronymic) =>
            await Client.PostAsJsonAsync(new Uri($"http://localhost:5182/person/{first_name},{last_name},{patronymic}"),
                (first_name, last_name, patronymic));

        public async Task Dal_DeletePerson_Async(int id) => await Client.DeleteAsync(new Uri($"{id}"));

        public async Task<int> Dal_GetPersonIdByFullName(string first_name, string last_name, string patronymic) =>
            await Client.GetFromJsonAsync<int>
            (new Uri($"http://localhost:5182/person/{first_name}, {last_name}, {patronymic}"));

        public async Task Dal_UpdateBook_Async(int id, string first_name, string last_name, string patronymic, int type_id, string number) =>
            await Client.PutAsJsonAsync
            (new Uri($"http://localhost:5182/book/{id},{first_name},{last_name},{patronymic},{type_id},{number}"), 
                (id, first_name, last_name, patronymic, type_id, number));

        public async Task Dal_UpdateBook_Async(int id, string first_name, string last_name, string patronymic) =>
            await Client.PutAsJsonAsync
            (new Uri($"http://localhost:5182/person/{id},{first_name},{last_name},{patronymic}"),
                (id, first_name, last_name, patronymic));
    }

}
