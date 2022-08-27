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

        var apiBaseUrl = "";
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            apiBaseUrl = "https://localhost:7012/";
        }
        else if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        {
            apiBaseUrl = "https://localhost:7012/";
        }
        else if (DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst)
        {
            apiBaseUrl = "https://localhost:7012/";
        }
        else if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            apiBaseUrl = "https://localhost:7012/";
        }
        else
        {
            apiBaseUrl = "https://localhost:7012/";
        }

        const string httpClientName = @"todoRestService";

        services.AddHttpClient(httpClientName, config => config.BaseAddress = new Uri(apiBaseUrl));
        services
            .AddScoped(sp => sp.GetService<IHttpClientFactory>()!.CreateClient(httpClientName))
            .AddSingleton<IRestDataService, RestDataService>()
            .AddSingleton<MainPage>()
            .AddTransient<ManageToDoPage>();

        return builder.Build();
    }
}
