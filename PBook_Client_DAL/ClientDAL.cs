
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
        //comment
        
    }

}
