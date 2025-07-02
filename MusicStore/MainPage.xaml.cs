using MusicStore.ViewModel;
using Syncfusion.Maui.Cards;

namespace MusicStore
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        private void SfCardLayout_VisibleIndexChanged(object sender, CardVisibleIndexChangedEventArgs e)
        {
            var sfCardLayout = sender as SfCardLayout;

            // When swiping past the first card (index becomes -1)
            if (sfCardLayout.VisibleIndex == 0)
            {
                // Set to the last card index (total cards - 1)
                sfCardLayout.VisibleIndex = sfCardLayout.Children.Count - 1;
            }

            // When swiping past the last card
            else if (sfCardLayout.VisibleIndex == sfCardLayout.Children.Count)
            {
                // Set to the first card index (0)
                sfCardLayout.VisibleIndex = 0;
            }
        }
    }

}
