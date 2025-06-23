using MusicStore.ViewModel;

namespace MusicStore.Controls;

public partial class FlyoutHeader : ContentView
{
	public FlyoutHeader()
	{
		InitializeComponent();
		BindingContext = new FlyoutHeaderVM();
	}
}