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
        collectionView.ItemsSource = toDos;
    }

    private async void OnAddToDoClickedAsync(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(ManageToDoPage), new Dictionary<string, object>
        {
            [nameof(ToDo)] = new ToDo(),
        });

    private async void OnSelectionChangedAsync(object sender, SelectionChangedEventArgs e) =>
        await Shell.Current.GoToAsync(nameof(ManageToDoPage), new Dictionary<string, object>
        {
            [nameof(ToDo)] = e?.CurrentSelection?[0] as ToDo,
        });
}
