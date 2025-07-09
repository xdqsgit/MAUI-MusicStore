using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KWApi;
using MusicStore.Model;
using MusicStore.Service;
using Plugin.Maui.Audio;

namespace MusicStore.ViewModels
{
    public abstract partial class AudioBaseVM : ObservableObject
    {

        /// <summary>
        /// 当前播放音乐
        /// </summary>
        [ObservableProperty]
        PlayListItem? currentMusicInfo;
        /// <summary>
        /// 当前播放歌曲歌词
        /// </summary>        
        public ObservableCollection<Lyric>? CurrentLyrics { get; set; }
        /// <summary>
        /// 当前播放列表
        /// </summary>
        public ObservableCollection<PlayListItem>? MusicQueue { get; set; }
        /// <summary>
        /// 当前播放索引
        /// </summary>
        [ObservableProperty]
        int currentMusicIndex;
        [ObservableProperty]
        bool isPlaying;

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
        public AudioBaseVM(KWApIHelper kWApIHelper)
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
        //public async Task PlayAudioAsync()
        //{
        //    var file = await FileSystem.OpenAppPackageFileAsync("ukelele.mp3");
        //    audioPlayer.SetSource(file);
        //    audioPlayer.Play();

        //}


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
                if (audioPlayer.Duration > 0)
                {
                    audioPlayer.Play();
                }
                audioPlayer.Play();
                DurationSeconds = audioPlayer.Duration;
                UpdateCurrentPosition();
            }
        }
        //下一曲
        [RelayCommand]
        void PlayNextMusic()
        {
            if (MusicQueue != null && MusicQueue.Count > 1)
            {
                if (CurrentMusicIndex + 1 < MusicQueue.Count - 1)
                {
                    CurrentMusicIndex++;
                    //todo 加载歌曲
                    PlayByIndex(CurrentMusicIndex);
                }
            }
        }
        //上一曲
        [RelayCommand]
        void PlayLastMusic()
        {
            if (MusicQueue != null && MusicQueue.Count > 1)
            {
                if (CurrentMusicIndex - 1 >= 0)
                {
                    CurrentMusicIndex--;
                    //todo 加载歌曲
                    PlayByIndex(CurrentMusicIndex);
                }
            }
        }

        [RelayCommand]
        void SliderDragCompleted()
        {
            if (audioPlayer.CanSeek)
            {
                if (CurrentSeconds != audioPlayer.CurrentPosition)
                    audioPlayer.Seek(CurrentSeconds);
            }
        }
        protected virtual async void PlayByIndex(int index)
        {
            CurrentMusicInfo = MusicQueue[index];
            await PlayCurrentMusic();
        }
        protected virtual async Task PlayCurrentMusic()
        {
            if (audioPlayer.IsPlaying)
            {
                audioPlayer.Stop();
            }

            await LoadMusicSourceAsync(CurrentMusicInfo);
            await LoadLyrics(CurrentMusicInfo);

            audioPlayer.Play();
            DurationSeconds = audioPlayer.Duration;
            UpdateCurrentPosition();
        }

        private async Task LoadLyrics(PlayListItem currentMusicInfo)
        {
            var lyrics = await kWApIHelper.GetLyricAsync(currentMusicInfo.Id);
            if (lyrics != null)
            {
                var list = lyrics.Lrclist.OrderBy(o => o.Time).Select((r, i) => new Lyric
                {
                    Order = i,
                    Time = r.Time,
                    Text = r.LineLyric

                });
                CurrentLyrics = new ObservableCollection<Lyric>(list);
            }
            else
            {
                CurrentLyrics = new ObservableCollection<Lyric>();
            }
        }

        private async Task LoadMusicSourceAsync(PlayListItem currentMusicInfo)
        {
            var playInfo = await kWApIHelper.GetPlayUrlAsync(currentMusicInfo.Id);
            if (playInfo != null)
            {
                var stream = await LoadFromUrlAsync(playInfo.Url ?? "");
                if (stream != null)
                {
                    audioPlayer.SetSource(stream);
                }
            }
            else
            {
                audioPlayer.SetSource(new MemoryStream());
            }
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
                    IsPlaying = audioPlayer.IsPlaying;
                    return;
                }
            }
            IsPlaying = audioPlayer.IsPlaying;
            CurrentSeconds = audioPlayer.CurrentPosition;
        }
    }
}
