using CommunityToolkit.Maui.Views;

namespace MusicStore
{
    public partial class App : Application
    {
		public static string ImageServerPath { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-.net-maui/common/uikitimages/";
        public static MediaElement MediaElement { get; internal set; }

        public App()
        {            
            InitializeComponent();
            MediaElement= new MediaElement();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
