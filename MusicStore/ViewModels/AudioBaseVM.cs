using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Storage;
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
        /// 当前榜单
        /// </summary>
        [ObservableProperty]
        PlayList? currentPlayList;
        /// <summary>
        /// 当前播放歌曲歌词全文
        /// </summary>        
        public ObservableCollection<Lyric>? CurrentLyrics { get; set; }
        /// <summary>
        /// 当前播放歌词
        /// </summary>
        [ObservableProperty]
        Lyric? currentLyric; 
        /// <summary>
        /// 当前播放列表
        /// </summary>
        public ObservableCollection<PlayListItem>? CurrentMusicQueue { get; set; }


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
        /// <summary>
        /// 是否有待播放音乐
        /// </summary>
        [ObservableProperty]
        bool hasMusic;

        partial void OnCurrentMusicIndexChanged(int val)
        {
            MusicPlayerManagerService.Instance.CurrentMusicIndex = val;
        }

        partial void OnCurrentMusicInfoChanged(PlayListItem val)
        {
            MusicPlayerManagerService.Instance.CurrentMusicInfo = val;
            HasMusic = (val != null);
        }

        partial void OnCurrentPlayListChanged(PlayList val)
        {
            MusicPlayerManagerService.Instance.CurrentPlayList = val;
        }

        partial void OnIsPlayingChanged(bool value)
        {
            MusicPlayerManagerService.Instance.IsPlaying = value;
        }

        KWApIHelper kWApIHelper;
        IAudioPlayer audioPlayer;
        public AudioBaseVM(KWApIHelper kWApIHelper)
        {
            audioPlayer = MusicPlayerManagerService.Instance.AudioPlayer;
            DurationSeconds = audioPlayer.Duration;
            CurrentMusicIndex = MusicPlayerManagerService.Instance.CurrentMusicIndex;
            //if (audioPlayer.IsPlaying)
            //{
            //    UpdateCurrentPosition();
            //}
            if (MusicPlayerManagerService.Instance.CurrentMusicInfo != null)
            {
                CurrentMusicInfo = MusicPlayerManagerService.Instance.CurrentMusicInfo;
            }
            if (MusicPlayerManagerService.Instance.MusicQueue != null)
            {
                CurrentMusicQueue = new ObservableCollection<PlayListItem>(MusicPlayerManagerService.Instance.MusicQueue);
            }
            if (MusicPlayerManagerService.Instance.CurrentLyrics != null)
            {
                CurrentLyrics = new ObservableCollection<Lyric>(MusicPlayerManagerService.Instance.CurrentLyrics);
            }
            IsPlaying = MusicPlayerManagerService.Instance.IsPlaying;
 
            this.kWApIHelper = kWApIHelper;

            if (IsPlaying)
            {
                UpdateCurrentPositionAsync();
            }
            //todo: 订阅播放完毕事件
            audioPlayer.PlaybackEnded += (s, e) =>
            {
                IsPlaying = false;
                //PlayNextMusic();
            };
        }

        async Task<Stream?> LoadFromUrlAsync(string url)
        {
            try
            {
                using var httpClient = new HttpClient();
                var httpResponse = await httpClient.GetAsync(url);
                httpResponse.EnsureSuccessStatusCode();
                string cacheDir = FileSystem.Current.CacheDirectory;
                var fileName = Path.Combine(cacheDir, CurrentMusicInfo.Id + ".mp3");
                await File.WriteAllBytesAsync(fileName, await httpResponse.Content.ReadAsByteArrayAsync());
                //保存成功
                return new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// 播放 或暂停
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        async Task PlayOrPause()
        {
            if (audioPlayer.IsPlaying)
            {
                audioPlayer.Pause();
                IsPlaying = false;
            }
            else
            {
                audioPlayer.Play();
                IsPlaying = true;
                DurationSeconds = audioPlayer.Duration;
                UpdateCurrentPositionAsync();
            }
        }
        //下一曲
        [RelayCommand]
        void PlayNextMusic()
        {
            if (CurrentMusicQueue != null && CurrentMusicQueue.Count > 1)
            {
                if (CurrentMusicIndex + 1 < CurrentMusicQueue.Count - 1)
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
            if (CurrentMusicQueue != null && CurrentMusicQueue.Count > 1)
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

        [RelayCommand]
        async Task ShowMusicDetailAsync()
        {
            await Shell.Current.GoToAsync("//MusicPlay", true);
        }


        protected virtual async void PlayByIndex(int index)
        {
            CurrentMusicInfo = CurrentMusicQueue[index];
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
            await Task.Delay(50);
            audioPlayer.Play();
            DurationSeconds = audioPlayer.Duration;
            UpdateCurrentPositionAsync();
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
                MusicPlayerManagerService.Instance.CurrentLyrics = list.ToList();
            }
            else
            {
                CurrentLyrics = new ObservableCollection<Lyric>();
                MusicPlayerManagerService.Instance.CurrentLyrics = new List<Lyric>();
            }

        }

        private async Task LoadMusicSourceAsync(PlayListItem currentMusicInfo)
        {
            //先在本地查找
            Stream? stream = null;
            var fileName = Path.Combine(FileSystem.CacheDirectory, CurrentMusicInfo.Id + ".mp3");
            if (File.Exists(fileName))
            {
                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            else
            {
                var playInfo = await kWApIHelper.GetPlayUrlAsync(currentMusicInfo.Id);
                if (playInfo != null)
                {
                    stream = await LoadFromUrlAsync(playInfo.Url ?? "");
                }
            }
            if (stream == null)
            {
                return;
            }
            else
            {
                audioPlayer.SetSource(stream ?? new MemoryStream());
            }
        }
        protected async void UpdateCurrentPositionAsync()
        {
            await Task.Delay(180);

            if (!audioPlayer.IsPlaying)
            {
                if (!audioPlayer.IsPlaying)
                {
                    IsPlaying = audioPlayer.IsPlaying;
                    return;
                }
            }

            IsPlaying = audioPlayer.IsPlaying;
            CurrentSeconds = audioPlayer.CurrentPosition;

            if(CurrentLyrics != null && CurrentLyrics.Count > 0)
            {
                var lyrc=CurrentLyrics.LastOrDefault(o => o.Time <= CurrentSeconds);
                if (lyrc != null)
                {
                    CurrentLyric = lyrc;
                }
            }

            if (CurrentSeconds >= DurationSeconds)
            {
                return;
            }
            Task.Run(() => UpdateCurrentPositionAsync());
        }
    }
}
