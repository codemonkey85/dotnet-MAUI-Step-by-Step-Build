namespace ToDoApi.Models;

public class ToDo
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
}
