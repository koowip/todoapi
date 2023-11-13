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
app.MapGet("/ToDo", async (ToDoContext db) => await db.ToDoItems.ToListAsync());

app.MapPost("/new", async (ToDoContext db, ToDoItem todo) => {
    
    db.ToDoItems.Add(todo);
    await db.SaveChangesAsync();
});

app.Run();
