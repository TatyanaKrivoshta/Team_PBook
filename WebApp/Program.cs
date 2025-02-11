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
//comment edit


app.Run();




