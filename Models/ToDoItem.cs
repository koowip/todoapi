using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todoapi.Models;

public class ToDoItem
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public long Id {get;set;}
  public string? Content {get;set;}
  public bool IsComplete {get;set;}
}