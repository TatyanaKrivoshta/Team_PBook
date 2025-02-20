using PBook_BL;
using PBook_Model;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHttpsRedirection();

var service = new Service();
app.MapGet("/persons", async () => await service.GetAllPerson_Async());
app.Run();


