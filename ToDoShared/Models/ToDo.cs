namespace ToDoShared.Models;

[ObservableObject]
public partial class ToDo
{
    [Key]
    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private string title = string.Empty;
}
