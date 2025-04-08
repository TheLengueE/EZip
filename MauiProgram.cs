using EZip.Controller;
using Microsoft.Extensions.Logging;
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

            // add Blazor support
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            // Singleton services  单例
            builder.Services.AddSingleton<LocalLanguageService>();
            //builder.Services.AddSingleton<EasyLogger>();
            builder.Services.AddSingleton(new EasyLogger("EZip.log", "MainLogger"));
            builder.Services.AddSingleton<DialogService>();
            builder.Services.AddSingleton<NotificationService>();
            builder.Services.AddSingleton<IPermissionHelper, AndroidPermissionHelper>();


            // when use visual studio, change different platform define different services
#if WINDOWS
            builder.Services.AddSingleton<ICompress,WindowsCompress>();
            builder.Services.AddSingleton<IDirectory, WindowsDirectoryOperations>();
            builder.Services.AddSingleton<IFile, WindowsFileOperations>();
#elif ANDROID
            builder.Services.AddSingleton<ICompress,AndroidCompress>();
            builder.Services.AddSingleton<IDirectory, AndroidDirectoryOperations>();
            builder.Services.AddSingleton<IFile, AndroidFileOperations>();
#endif

            // Transient services  每次请求都会创建一个新的实例

            // Scoped services   每个作用域创建一个新的实例
            //builder.Services.AddScoped<NotificationService>();

            return builder.Build();
        }
    }
}
