using Microsoft.AspNetCore.Builder;
using PBook_BL;
using PBook_Model;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();
var service = new Service();

app.MapGet("/books", async () => await service.GetAllBook_Async());

app.MapGet("/book/{id}", async(int id) => await service.GetBookById_Async(id));

app.MapPost("/book/",
    async (Book contact) =>
    await service.AddBook_Async(contact.Person.FirstName, contact.Person.LastName, contact.Person.Patronymic, contact.Type.Id, contact.Number));

app.MapDelete("/book/{id}", async(int id)=> await service.DeleteBook_Async(id));

// ������� ��������

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




