using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using MusicStore.Model;
using Plugin.Maui.Audio;

namespace MusicStore.Service
{
    /// <summary>
    /// 播放管理服务
    /// </summary>
    public class MusicPlayerManagerService
    {
        private static readonly Lazy<MusicPlayerManagerService> lazy = new Lazy<MusicPlayerManagerService>(() => new MusicPlayerManagerService());

        public static MusicPlayerManagerService Instance => lazy.Value;

        public IAudioPlayer AudioPlayer { get;private set; }

        private MusicPlayerManagerService()
        {
            AudioPlayer = AudioManager.Current.CreatePlayer();            
        }

        public int CurrentMusicIndex { get; private set; }
        /// <summary>
        /// 歌单信息
        /// </summary>
        public PlayList PlayList{ get;private set; }
        /// <summary>
        /// 歌曲列表
        /// </summary>
        public List<PlayListItem> PlayListItems { get; private set; }
        /// <summary>
        /// 当前歌词
        /// </summary>
        public List<Lyric>? CurrentLyrics { get; private set; }
        public PlayListItem? CurrentMusicInfo { get; private set; }
        public List<PlayListItem>? MusicQueue { get; private set; }
    }
}
