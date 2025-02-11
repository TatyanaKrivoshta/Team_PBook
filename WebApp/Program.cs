using Microsoft.AspNetCore.Builder;
using PBook_BL;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();
var service = new Service();

app.MapGet("/books", async () => await service.GetAllBook_Async());

app.MapGet("/book/{id}", async(int id) => await service.GetBookById_Async(id));

app.MapPost("/book/{first_name},{last_name},{patronymic},{type_id},{number}",
    async (string first_name, string last_name, string patronymic, int type_id, string number) =>
    await service.AddBook_Async(first_name, last_name, patronymic, type_id, number));

app.MapDelete("/book/{id}", async(int id)=> await service.DeleteBook_Async(id));

// Добавил заглушки

app.MapGet("/persons", async () => await service.GetAllPerson_Async());

app.MapGet("/person/{id}", async (int id) => await service.GetPersonById_Async(id));

app.MapGet("/phoneTypes", async () => await service.GetAllPhoneType_Async());

app.MapGet("/phoneType/{id}", async (int id) => await service.GetPhoneTypeById_Async(id));

app.MapPost("/person/{first_name},{last_name},{patronymic}", 
    async (string first_name, string last_name, string patronymic) => 
    await service.AddPerson(first_name, last_name, patronymic));

app.MapDelete("/person/{id}", async (int id) => await service.DeletePerson_Async(id));

app.MapGet("/person/{first_name},{last_name},{patronymic}", 
    async(string first_name, string last_name, string patronymic) => 
    await service.GetPersonIdByFullName(first_name, last_name, patronymic));

app.Run();




