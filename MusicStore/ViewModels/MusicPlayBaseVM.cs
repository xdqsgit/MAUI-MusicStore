using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KWApi;
using MusicStore.Model;
using MusicStore.Service;
using Plugin.Maui.Audio;

namespace MusicStore.ViewModels
{
    public abstract partial class MusicPlayBaseVM : ObservableObject
    {
        /// <summary>
        /// 当前播放音乐
        /// </summary>
        [ObservableProperty]
        PlayListItem? currentMusicInfo;
        /// <summary>
        /// 当前播放歌曲歌词
        /// </summary>
        ObservableCollection<Lyric>? CurrentLyrics { get; set; }
        /// <summary>
        /// 当前播放列表
        /// </summary>
        ObservableCollection<PlayListItem>? MusicQueue { get; set; }
        /// <summary>
        /// 当前播放索引
        /// </summary>
        [ObservableProperty]
        int currentMusicIndex;

        /// <summary>
        /// 媒体时长
        /// </summary>
        [ObservableProperty]
        double durationSeconds;

        /// <summary>
        /// 当前播放位置
        /// </summary>
        [ObservableProperty]
        double currentSeconds;

        KWApIHelper kWApIHelper;
        IAudioPlayer audioPlayer;
        public MusicPlayBaseVM(KWApIHelper kWApIHelper)
        {
            audioPlayer = MusicPlayerManagerService.Instance.AudioPlayer;
            DurationSeconds = audioPlayer.Duration;
            CurrentMusicIndex = MusicPlayerManagerService.Instance.CurrentMusicIndex;
            if (audioPlayer.IsPlaying)
            {
                UpdateCurrentPosition();
            }
            if (MusicPlayerManagerService.Instance.CurrentMusicInfo != null)
            {
                CurrentMusicInfo = MusicPlayerManagerService.Instance.CurrentMusicInfo;
            }
            if (MusicPlayerManagerService.Instance.MusicQueue != null)
            {
                MusicQueue = new ObservableCollection<PlayListItem>(MusicPlayerManagerService.Instance.MusicQueue);
            }
            if (MusicPlayerManagerService.Instance.CurrentLyrics != null)
            {
                CurrentLyrics = new ObservableCollection<Lyric>(MusicPlayerManagerService.Instance.CurrentLyrics);
            }


            CurrentLyrics =
            [
                new Lyric { Time = 0, Text = "热爱105°C的你 - 阿肆" },
                new Lyric { Time = 0.1f, Text = "词：阿肆" },
                new Lyric { Time = 0.54f, Text = "混音室：唐汉霄工作室" },
                new Lyric { Time = 0.73f, Text = "Super Idol的笑容" },
                new Lyric { Time = 2.26f, Text = "都没你的甜" },
                new Lyric { Time = 4.02f, Text = "八月正午的阳光" },
                new Lyric { Time = 5.86f, Text = "都没你耀眼" },
                new Lyric { Time = 7.53f, Text = "热爱 105 °C的你" },
                new Lyric { Time = 10.11f, Text = "滴滴清纯的蒸馏水" },
            ];
            this.kWApIHelper = kWApIHelper;

            //订阅列表切换事件
            //上一曲 下一曲事件
        }
        public async Task PlayAudioAsync()
        {
            var file = await FileSystem.OpenAppPackageFileAsync("ukelele.mp3");
            audioPlayer.SetSource(file);
            audioPlayer.Play();
        }


        async Task<Stream> LoadFromUrlAsync(string url)
        {
            using var httpClient = new HttpClient();
            var stream = await httpClient.GetStreamAsync(url);
            return stream;
        }
        /// <summary>
        /// 播放 或暂停
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        async void PlayOrPause()
        {
            if (audioPlayer.IsPlaying)
            {
                audioPlayer.Pause();
            }
            else
            {
                audioPlayer.Play();
                DurationSeconds = audioPlayer.Duration;
                UpdateCurrentPosition();
            }
        }
        //下一曲
        [RelayCommand]
        void NextMusic()
        {
            if (MusicQueue != null&&MusicQueue.Count > 0)
            {
                if (CurrentMusicIndex < MusicQueue.Count - 1)
                {   
                    CurrentMusicIndex++;
                    //todo 加载歌曲
                    PlayByIndex(CurrentMusicIndex);
                }
            }
        }

        protected virtual void PlayByIndex(int index) 
        {
            if (audioPlayer.IsPlaying)
            {
                audioPlayer.Stop();
            }
            
            CurrentMusicInfo = MusicQueue[index];
            LoadMusicSource(CurrentMusicInfo);
            LoadLyrics(CurrentMusicInfo);            
        }

        private void LoadLyrics(PlayListItem currentMusicInfo)
        {
            throw new NotImplementedException();
        }

        private void LoadMusicSource(PlayListItem currentMusicInfo)
        {
            kWApIHelper.get
        }

        private void UpdateCurrentPosition()
        {
            if (!audioPlayer.IsPlaying)
            {
#if WINDOWS
                Thread.Sleep(50);
#endif
                if (!audioPlayer.IsPlaying)
                {
                    return;
                }
            }

            CurrentSeconds = audioPlayer.CurrentPosition;
        }
    }
}
