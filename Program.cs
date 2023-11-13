using Microsoft.EntityFrameworkCore;
using todoapi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ToDoContext>(opt => opt.UseInMemoryDatabase("TodoList"));

//API documentation 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.MapGet("/", () => "Landing page");
app.MapPost("/newToDo/{todoContent}", async (DbContext db, string todoString) => {
    ToDoItem todo = new ToDoItem();
    todo.Content = todoString;
    todo.IsComplete = false;

    db.Add(todo);
    await db.SaveChangesAsync();
});

app.Run();
