namespace todoapi.Models;

public class ToDoItem
{
  public long Id {get;set;}
  public string? Content {get;set;}
  public bool IsComplete {get;set;}
}