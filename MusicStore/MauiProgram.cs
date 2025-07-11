using CommunityToolkit.Maui;
using KWApi;
using Microsoft.Extensions.Logging;
using MusicStore.ViewModel;
using MusicStore.ViewModels;
using MusicStore.Views;
using Plugin.Maui.Audio;
using Syncfusion.Maui.Core.Hosting;

using Syncfusion.Maui.Toolkit.Hosting;
using ZXing.Net.Maui.Controls;
namespace MusicStore
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMediaElement()
                .UseBarcodeReader()
                .UseMauiCommunityToolkit()
                .AddAudio()
				.ConfigureSyncfusionToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Roboto-Medium.ttf", "Roboto-Medium");
                    fonts.AddFont("Roboto-Regular.ttf", "Roboto-Regular");
                    fonts.AddFont("UIFontIcons.ttf", "FontIcons");
                    fonts.AddFont("Dashboard.ttf", "DashboardFontIcons");
                });
            //Register Syncfusion license https://help.syncfusion.com/common/essential-studio/licensing/how-to-generate
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzk0MjIzMkAzMzMwMmUzMDJlMzAzYjMzMzAzYkFnZFJGNkw0eUZidWxINzdlQ2Yrc2dremtOQXdxd0l0MjFIQlhOUFNWL3M9");

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<KWApIHelper>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MusicPlay>();

            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MusicPalyVM>();

            //Â·ÓÉ×¢²á
            Routing.RegisterRoute(nameof(MusicPlay), typeof(MusicPlay));

            return builder.Build();
        }
    }
}
