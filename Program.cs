using EmployeesApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

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
app.MapPut("/employees/{id}",(int id, Employee updateData) =>
{
    var employee = employees.FirstOrDefault(x => x.Id == id);
    if (employee is null)
    {
        return Results.NotFound();
    }

    employee.Name = updateData.Name;
    employee.Position = updateData.Position;
    employee.Salary = updateData.Salary;
    return Results.NoContent();
});

app.MapDelete("employees/{id}", (int id) =>
{
    var employee = employees.FirstOrDefault(x => x.Id == id);
    if (employee is null)
    {
        return Results.NotFound();
    }
    if (employee == null)
    {
        return Results.NotFound();
    }
    employees.Remove(employee);
    return Results.NoContent();
});






app.Run();
