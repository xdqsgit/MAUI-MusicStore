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
        [ObservableProperty]
        private ObservableCollection<BannerInfo> bannerList = new ObservableCollection<BannerInfo>();

        [ObservableProperty]
        private ObservableCollection<RcmPlayListItem> dayRmcList = new ObservableCollection<RcmPlayListItem>();

        [ObservableProperty]
        private ObservableCollection<KWBangList> bangList = new ObservableCollection<KWBangList>();

        [ObservableProperty]
        private bool isSearchResultVisible;
        [ObservableProperty]
        ObservableCollection<ListData>? searchResult;
        public MainPageViewModel(KWApIHelper kWApIHelper)
        {
            KWApIHelper = kWApIHelper;
            LoadData();
        }

        private void LoadData()
        {
            LoadBanner();
            LoadDayRmcPlayList();
            LoadBangList();
        }

        private async void LoadDayRmcPlayList()
        {
            var dayRmcPlayList = await KWApIHelper.GetDayRcmPlayListAsync();
            if (dayRmcPlayList != null && dayRmcPlayList.List != null)
            {
                DayRmcList = new ObservableCollection<RcmPlayListItem>(dayRmcPlayList.List);
            }
        }

        public async void LoadBanner()
        {
            var banners = await KWApIHelper.GetBannerListAsync();
            if (banners != null && banners!= null)
            {
                BannerList = new ObservableCollection<BannerInfo>(banners.Skip(2));
            }

        }

        public async void LoadBangList()
        {
            var bangList = await KWApIHelper.GetBangListAsync();
            if (bangList != null)
            {
                BangList = new ObservableCollection<KWBangList>(bangList);
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

        [RelayCommand]
        void ShowShell()
        {
            Shell.Current.FlyoutIsPresented = true;
        }


        [RelayCommand]
        void Search(string searchText)
        {
            var result = SearchData.Where(x => x.Name.Contains(searchText)).ToList();
            SearchResult = new ObservableCollection<ListData>(result);
            IsSearchResultVisible = true;
        }

    }
}
