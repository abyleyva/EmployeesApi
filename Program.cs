using EmployeesApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var Employees = new List<Employee>();

app.MapGet("/", () => "Hello World!");




app.Run();
