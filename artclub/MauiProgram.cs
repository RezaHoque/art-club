﻿using artclub.Data;
using artclub.Pages;
using Microsoft.Extensions.Logging;
using UraniumUI;

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
                    fonts.AddFontAwesomeIconFonts();
                });
            builder.Services.AddSingleton<DbService>();
            builder.Services.AddTransient<HomePage>();
            builder.UseUraniumUI();
            builder.UseUraniumUIMaterial();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
