using EmployeesApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var employees = new List<Employee>();

app.MapGet("/employees", () => employees);

app.MapGet("/employees/{id}", (int id) =>
employees.FirstOrDefault(e => e.Id == id) is Employee e ? Results.Ok(e) : Results.NotFound()
/* 
same as above
{
    var employee = employees.FirstOrDefault(x => x.Id == id);
    if (employee is  not null)
    {
        return Results.Ok(employee);
    }
    return Results.NotFound();
} */
);

app.MapPost("/employees", (Employee employee) =>
{
    employee.Id = employees.Count > 0 ? employees.Max(e => e.Id) + 1 : 1;
    employees.Add(employee);
    return Results.Created($"/employees/{employee.Id}", employee);
}
);






app.Run();
