namespace ToDoMauiClient;

public partial class MainPage : ContentPage
{
    private readonly IRestDataService dataService;

    public MainPage(IRestDataService dataService)
    {
        InitializeComponent();
        this.dataService = dataService;
    }

    protected async override void OnAppearing() 
    {
        base.OnAppearing();
        var toDos = await dataService.GetAllToDosAsync();
        foreach (var toDo in toDos) 
        {
            Debug.WriteLine($"{toDo.Id} - {toDo.Title}");
        }
    }
}
