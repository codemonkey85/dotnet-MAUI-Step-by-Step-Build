using ToDoShared.Models;

namespace ToDoMauiClient.Services;

public record RestDataService(HttpClient HttpClient) : IRestDataService
{
    public async Task<ToDo[]> GetAllToDosAsync()
    {
        if (!CheckInternetConnectivity())
        {
            Debug.WriteLine("No internet access.");
            return Array.Empty<ToDo>();
        }
        try
        {
            var response = await HttpClient.GetFromJsonAsync<ToDo[]>(ToDoEndpointUrl);
            if (response is null)
            {
                Debug.WriteLine("Something went wrong.");
            }
            return response;
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return Array.Empty<ToDo>();
        }
    }

    public async Task<ToDo> GetToDoAsync(int id)
    {
        if (!CheckInternetConnectivity())
        {
            Debug.WriteLine("No internet access.");
            return null;
        }
        try
        {
            var response = await HttpClient.GetFromJsonAsync<ToDo>($"{ToDoEndpointUrl}/{id}");
            if (response is null)
            {
                Debug.WriteLine("Something went wrong.");
            }
            return response;
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return null;
        }
    }

    public async Task<ToDo> CreateToDoAsync(ToDo todo)
    {
        if (!CheckInternetConnectivity())
        {
            Debug.WriteLine("No internet access.");
            return null;
        }
        try
        {
            var response = await HttpClient.PostAsJsonAsync(ToDoEndpointUrl, todo);
            if (response is null)
            {
                Debug.WriteLine("Something went wrong.");
            }
            var createdToDo = await response.Content.ReadFromJsonAsync<ToDo>();
            return createdToDo;
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return null;
        }
    }

    public async Task<ToDo> UpdateToDoAsync(ToDo todo)
    {
        if (!CheckInternetConnectivity())
        {
            Debug.WriteLine("No internet access.");
            return null;
        }
        try
        {
            var response = await HttpClient.PutAsJsonAsync($"{ToDoEndpointUrl}/{todo.Id}", todo);
            if (response is null)
            {
                Debug.WriteLine("Something went wrong.");
            }
            return todo;
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return null;
        }
    }

    public async Task DeleteToDoAsync(int id)
    {
        if (!CheckInternetConnectivity())
        {
            Debug.WriteLine("No internet access.");
            return;
        }
        try
        {
            var response = await HttpClient.DeleteAsync($"{ToDoEndpointUrl}/{id}");
            if (response is null)
            {
                Debug.WriteLine("Something went wrong.");
            }
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return;
        }
    }

    private static bool CheckInternetConnectivity() => Connectivity.Current.NetworkAccess == NetworkAccess.Internet;

    private static void HandleException(Exception ex) => Debug.WriteLine(ex);
}
