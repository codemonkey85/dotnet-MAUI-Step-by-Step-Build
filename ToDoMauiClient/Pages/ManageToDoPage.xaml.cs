namespace ToDoMauiClient.Pages;

[QueryProperty(nameof(ToDo), nameof(ToDo))]
public partial class ManageToDoPage : ContentPage
{
    private readonly IRestDataService dataService;
    private ToDo toDo;
    private bool isNew;

    public ToDo ToDo
    {
        get => toDo;
        set
        {
            isNew = IsNew(value);
            toDo = value;
            OnPropertyChanged();
        }
    }

    public ManageToDoPage(IRestDataService restDataService)
    {
        InitializeComponent();
        dataService = restDataService;
        BindingContext = this;
    }

    private static bool IsNew(ToDo toDo) => toDo is { Id: 0 };

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        Func<ToDo, Task<ToDo>> createOrUpdateToDo = isNew switch
        {
            true => dataService.CreateToDoAsync,
            false => dataService.UpdateToDoAsync,
        };
        await createOrUpdateToDo(ToDo);
        await Shell.Current.GoToAsync("..");
    }

    private async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        await dataService.DeleteToDoAsync(ToDo.Id);
        await Shell.Current.GoToAsync("..");
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync("..");
}
