namespace ToDoMauiClient.Services;

public interface IRestDataService
{
    Task<ToDo[]> GetAllToDosAsync();
    Task<ToDo> GetToDoAsync(int id);
    Task<ToDo> CreateToDoAsync(ToDo todo);
    Task<ToDo> UpdateToDoAsync(ToDo todo);
    Task DeleteToDoAsync(int id);
}
