using artclub.Data;
using artclub.Pages;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Microsoft.Extensions.Logging;

namespace artclub
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FASolid");
                });
            builder.UseMauiCommunityToolkit();
            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);


            builder.Services.AddSingleton<DbService>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<MemberPage>();
            builder.Services.AddTransient<ArtPage>();
            builder.Services.AddTransient<LotteryPage>();
            //builder.UseUraniumUI();
           // builder.UseUraniumUIMaterial();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
