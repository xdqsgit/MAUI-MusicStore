using CommunityToolkit.Maui;
using KWApi;
using Microsoft.Extensions.Logging;
using MusicStore.ViewModel;
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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NNaF1cWWhPYVFwWmFZfVtgdVVMZFtbRn5PIiBoS35Rc0VlWHZccnBSRGhcUUN/VEBU");

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<KWApIHelper>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddTransient<MainPageViewModel>();

            return builder.Build();
        }
    }
}
