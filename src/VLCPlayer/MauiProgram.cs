using Microsoft.Extensions.Logging;
using VLCPlayer.Controls;
using VLCPlayer.Handlers;

namespace VLCPlayer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMauiHandlers((handlers) =>
                {
                    handlers.AddHandler(typeof(MediaViewer), typeof(MediaViewerHandler));
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
