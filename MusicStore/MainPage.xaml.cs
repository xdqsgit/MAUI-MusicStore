using System.Timers;
using KWApi.Models;
using Microsoft.Maui.Controls;
using MusicStore.ViewModel;
using Syncfusion.Maui.Cards;

namespace MusicStore
{
    public partial class MainPage : ContentPage
    {
        System.Timers.Timer timer;
        int currentIndex = 0;
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            timer = new System.Timers.Timer
            {
                Interval = 1000 * 3
            };
            timer.Elapsed += Scroll;
            timer.Start();
        }

        private void Scroll(object? sender, ElapsedEventArgs e)
        {
            currentIndex++;
            if (currentIndex == 8)
            {
                currentIndex = 0;
            }
            Dispatcher.Dispatch(() => carouselView.ScrollTo(currentIndex));
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
        public void BangList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void SfCarousel_SelectionChanged(object sender, Syncfusion.Maui.Core.Carousel.SelectionChangedEventArgs e)
        {
            if (e.NewItem is BannerInfo bannerInfo)
            {
                //banner_label.Text = bannerInfo.NewPicText;
            }
        }
    }

}
