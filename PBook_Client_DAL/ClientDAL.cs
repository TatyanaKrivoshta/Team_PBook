
using PBook_Model;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace PBook_Client_DAL
{
    public class ClientDAL
    {
        private static readonly HttpClient Client = new();

        public ObservableCollection<Book> books;
        public ObservableCollection<Person> persons; // добавил коллекцию персон
        public ObservableCollection<PhoneType> phoneTypes;
        public ClientDAL()
        {
            books = new ObservableCollection<Book>();
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

        // Добавил апдейты

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
