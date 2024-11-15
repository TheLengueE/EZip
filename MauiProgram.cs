using EZip.Controller;
using Radzen;

namespace EZip
{
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
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif


            // Singleton services
            builder.Services.AddSingleton<LocalLanguageService>();
            builder.Services.AddSingleton<EasyLogger>();

            builder.Services.AddSingleton<DialogService>();
            builder.Services.AddSingleton<NotificationService>();

            // Transient services

            // Scoped services

            return builder.Build();
        }
    }
}
