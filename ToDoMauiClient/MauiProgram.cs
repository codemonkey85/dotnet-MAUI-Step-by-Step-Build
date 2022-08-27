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

        const string httpClientName = @"todoRestService";

        var apiBaseUrl = DeviceInfo.Current.Platform switch
        {
            var platform when platform == DevicePlatform.Android => "https://localhost:7012/",
            var platform when platform == DevicePlatform.iOS => "https://localhost:7012/",
            var platform when platform == DevicePlatform.MacCatalyst => "https://localhost:7012/",
            var platform when platform == DevicePlatform.WinUI => "https://localhost:7012/",
            _ => "https://localhost:7012/",
        };

        services.AddHttpClient(httpClientName, config => config.BaseAddress = new Uri(apiBaseUrl));
        services
            .AddScoped(sp => sp.GetService<IHttpClientFactory>()!.CreateClient(httpClientName))
            .AddSingleton<IRestDataService, RestDataService>()
            .AddSingleton<MainPage>()
            .AddTransient<ManageToDoPage>();

        return builder.Build();
    }
}
