using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KWApi;
using KWApi.Models;
using MusicStore.Model;
using MusicStore.Service;
using MusicStore.ViewModels;

namespace MusicStore.ViewModel
{
    public partial class MainPageViewModel : AudioBaseVM
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
        /// <summary>
        /// 选中的榜单歌曲
        /// </summary>
        [ObservableProperty]
        MusicList? selectedBangItem;
        public MainPageViewModel(KWApIHelper kWApIHelper):base(kWApIHelper)
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
            if (banners != null && banners != null)
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

        [RelayCommand]
        async Task BangItemSelectedAsync(KWBangList selectedList)
        {           
            //设置播放列表 和 当前播放歌曲
            //跳转到播放界面
            var musicList= selectedList.MusicList.Select(r => new PlayListItem
            {
                Album = r.Album,
                Artist = r.Artist,
                Id = r.Rid,
                Name = r.Name,
                Pic = r.Pic,
                Duration = r.Duration
            }).ToList();

            CurrentMusicQueue = new ObservableCollection<PlayListItem>(musicList);
            MusicPlayerManagerService.Instance.MusicQueue = musicList;

            CurrentPlayList = new PlayList
            {
                Name = selectedList.Name,
                Id = selectedList.Id,
                Pic = selectedList.Pic
            };

            CurrentMusicInfo = new PlayListItem
            {
                Album = selectedBangItem.Album,
                Artist = selectedBangItem.Artist,
                Id = selectedBangItem.Rid,
                Name = selectedBangItem.Name,
                Pic = selectedBangItem.Pic,
                Duration = selectedBangItem.Duration
            };

            await PlayCurrentMusic();
        }
    }
}
