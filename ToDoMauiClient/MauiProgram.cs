namespace ToDoMauiClient;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var services = builder.Services;

        services.AddHttpClient("todoRestService", config => config.BaseAddress = new Uri("https://localhost:7012/"));
        services.AddSingleton<IRestDataService, RestDataService>();

        return builder.Build();
    }
}
