using MusicStore.ViewModel;
using MusicStore.Views;

namespace MusicStore.Controls;

public partial class FlyoutHeader : ContentView
{
    public FlyoutHeader()
    {
        InitializeComponent();
        BindingContext = new FlyoutHeaderVM();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var page = new ZXingPage();
        //await Window.Navigation.PushAsync(page);
        await Shell.Current.Navigation.PushModalAsync(page);
    }
}