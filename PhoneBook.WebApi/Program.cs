using PhoneBook.BL;
using PhoneBook.Model;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();

var service = new PhoneBookService(); // создаем запросы ссылаясь на слой ToDo.BL

app.MapGet("/persons", async () => await service.GetAllPersonsAsync());
app.MapGet("/phones", async () => await service.GetAllPhonesAsync());
app.MapGet("/persons_and_phones", async () => await service.GetAllPersonsWithPhonesAsync());

app.MapPost("/persons", async (Person person) => await service.CreatePersonAsync(person));
app.MapPut("/persons", async (Person person) => await service.UpdatePersonAsync(person));
app.MapDelete("/persons/{id:int}", async (int id) => await service.DeletePersonAsync(id));

app.Run();