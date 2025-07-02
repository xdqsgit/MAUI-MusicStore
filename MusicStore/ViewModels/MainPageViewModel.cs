using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KWApi;
using KWApi.Models;
using MusicStore.Model;

namespace MusicStore.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        KWApIHelper KWApIHelper;

        public MainPageViewModel(KWApIHelper kWApIHelper)
        {
            KWApIHelper = kWApIHelper;
            LoadData();
        }

        private void LoadData()
        {
            LoadBanner();
            LoadBangList();
        }

        public async void LoadBanner()
        {
            //ImageCollection.Add(new CarouselModel("carousel_person1.jpg"));
            //ImageCollection.Add(new CarouselModel("carousel_person2.png"));
            //ImageCollection.Add(new CarouselModel("carousel_person3.jpg"));
            //ImageCollection.Add(new CarouselModel("carousel_person4.jpg"));
            //ImageCollection.Add(new CarouselModel("carousel_person5.png"));
            //ImageCollection.Add(new CarouselModel("carousel_person6.gif"));
            //ImageCollection.Add(new CarouselModel("dotnet_bot.png"));

            var bannerList = await KWApIHelper.GetRcmPlayList(10);
            if (bannerList != null && bannerList.Data != null)
            {
                BannerList=new ObservableCollection<RcmPlayListItem>(bannerList.Data);
            }

        }

        public async void LoadBangList()
        {
            var bangList = await KWApIHelper.GetBangListAsync();
            if (bangList != null)
            {
                BangList=new ObservableCollection<KWBangList>(bangList);
            }
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
        private ObservableCollection<RcmPlayListItem> bannerList = new ObservableCollection<RcmPlayListItem>();

        [ObservableProperty]
        private ObservableCollection<KWBangList> bangList = new ObservableCollection<KWBangList>();

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
