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

//app.UseHttpsRedirection();
//app.UseAuthentication();


app.MapGet("/", () => "Landing page");
app.MapGet("/allToDo", async (ToDoContext db) => await db.ToDoItems.ToListAsync());
app.MapGet("/toDo/{id}", async (ToDoContext db, long id) => {
    var todo = await db.ToDoItems.FindAsync(id);
    return todo;
});
app.MapPost("/new", async (ToDoContext db, ToDoItem todo) => { 
    db.ToDoItems.Add(todo);
    await db.SaveChangesAsync();
});

app.MapPut("/edit", async (ToDoContext db, ToDoItem todonew) => {
    var todo = await db.ToDoItems.FindAsync(todonew.Id);
    if (todo != null) 
    {
        todo.Content = todonew.Content;
        todo.IsComplete = todonew.IsComplete;
        await db.SaveChangesAsync();
    }
});

app.MapDelete("/del/{id}", async (ToDoContext db, long id) => {
    var todo = await db.ToDoItems.FindAsync(id);
    if (todo != null) 
    {
        db.Remove(todo);
        await db.SaveChangesAsync();
    }
});

app.Run();
