using MapDemo.Map;
using Maui.GoogleMaps.Hosting;
using Microsoft.Extensions.Logging;

namespace MapDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
#if ANDROID

            .UseGoogleMaps()
            .ConfigureMauiHandlers((IMauiHandlersCollection handlers) =>
            {
                handlers.AddTransient(typeof(ClusterMap), (IServiceProvider h) => new ClusterMapHandler());               
            })
#elif IOS
            .UseGoogleMaps("PUT YOUR KEY HERE")
            .ConfigureMauiHandlers((IMauiHandlersCollection handlers) =>
            {
                handlers.AddTransient(typeof(ClusterMap), (IServiceProvider h) => new ClusterMapHandler());               
            })
           
#endif
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            return builder.Build();
        }
    }
}