using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicStore.Model;

namespace MusicStore.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel()
        {
            ImageCollection.Add(new CarouselModel("carousel_person1.jpg"));
            ImageCollection.Add(new CarouselModel("carousel_person2.png"));
            ImageCollection.Add(new CarouselModel("dotnet_bot.png"));
            ImageCollection.Add(new CarouselModel("carousel_person3.jgp"));
            //ImageCollection.Add(new CarouselModel("carousel_person4.png"));
            //ImageCollection.Add(new CarouselModel("carousel_person5.png"));
        
        }
        public int DayOfMonth => DateTime.Now.Day;
        private List<ListData> SearchData = new List<ListData>() { new ListData("Adele"),
            new ListData("Adele1"),
            new ListData("bdele"),
            new ListData("Bdele"),
            new ListData("cdele"),
            new ListData("Ddele"),
        };
        [ObservableProperty]
        private ObservableCollection<CarouselModel>? imageCollection = new ObservableCollection<CarouselModel>();

        [ObservableProperty]
        private bool isSearchResultVisible;

        [RelayCommand]
        void ShowShell()
        {
            Shell.Current.FlyoutIsPresented = true;
        }

        [ObservableProperty]
        ObservableCollection<ListData>? searchResult;
        [RelayCommand]
        void Search(string searchText)
        {
            var result = SearchData.Where(x => x.Name.Contains(searchText)).ToList();
            SearchResult = new ObservableCollection<ListData>(result);
            IsSearchResultVisible = true;
        }

    }
}
