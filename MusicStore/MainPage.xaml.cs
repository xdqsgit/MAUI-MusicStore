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

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            timer.Stop();
            timer.Dispose();
        }
    }

}
