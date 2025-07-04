using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using MusicStore.Model;

namespace MusicStore.Views;

public partial class MusicPlay : ContentPage
{
    public List<Lyric> Lyrics { get; set; }= new List<Lyric>();
    //MediaElement mediaElement;
    public MusicPlay()
    {
        InitializeComponent();
        Lyrics.Add(new Lyric { Time = 0, Text = "热爱105°C的你 - 阿肆" });
        Lyrics.Add(new Lyric { Time = 0.1f, Text = "词：阿肆" });
        Lyrics.Add(new Lyric { Time = 0.54f, Text = "混音室：唐汉霄工作室" });
        Lyrics.Add(new Lyric { Time = 0.73f, Text = "Super Idol的笑容" });
        Lyrics.Add(new Lyric { Time = 2.26f, Text = "都没你的甜" });
        Lyrics.Add(new Lyric { Time = 4.02f, Text = "八月正午的阳光" });
        Lyrics.Add(new Lyric { Time = 5.86f, Text = "都没你耀眼" });
        Lyrics.Add(new Lyric { Time = 7.53f, Text = "热爱 105 °C的你" });
        Lyrics.Add(new Lyric { Time = 10.11f, Text = "滴滴清纯的蒸馏水" });
        BindableLayout.SetItemsSource(LyricList, Lyrics);
        //mediaElement = new MediaElement();

        // 设置音乐文件路径（可从本地或网络加载）
        mediaElement.Source = MediaSource.FromFile(@"E:\testProject\M5000040uYbR1AkIDA.mp3"); // 替换为实际音乐文件路径

        mediaElement.Volume = 0.8; // 设置默认音量
        mediaElement.MediaOpened += MediaOpened; // 监听媒体打开事件
        mediaElement.PositionChanged += MediaPositionChanged; // 监听播放位置变化事件
        mediaElement.MediaFailed += MediaFailed; // 监听媒体加载失败事件
    }

    private async void MediaFailed(object? sender, MediaFailedEventArgs e)
    {
        await DisplayAlert("提示", "音乐文件加载失败", "确定");
    }

    private void MediaOpened(object? sender, EventArgs e)
    {
        Dispatcher.Dispatch(() =>
        {
            // 音乐加载完成后可执行的操作，如更新封面等            
            var duration = mediaElement.Duration;
            ProgressSlider.Maximum = duration.TotalSeconds;
            ProgressSlider.Value = 0;

            CurrentTimeLabel.Text = "0:00";
            DurationLabel.Text = duration.ToString(@"mm\:ss");
        });

    }

    private void MediaPositionChanged(object? sender, MediaPositionChangedEventArgs e)
    {
        Dispatcher.Dispatch(() =>
        {
            // 更新进度条
            ProgressSlider.Value = mediaElement.Position.TotalSeconds;
            CurrentTimeLabel.Text = mediaElement.Position.ToString(@"mm\:ss");

        });
    }
     
    private async void OnPlayPauseClicked(object sender, EventArgs e)
    {

        if (mediaElement.CurrentState == MediaElementState.None
                || mediaElement.CurrentState == MediaElementState.Failed)
        { 
            await DisplayAlert("提示", "音乐文件加载失败", "确定");
            return;
        }
            if (mediaElement.CurrentState == MediaElementState.Playing)
        {
            mediaElement.Pause();
            PlayPauseButton.Text = "▶";
        }
        else
        {
            mediaElement.Play();
            PlayPauseButton.Text = "⏸";
        }
    }

    private void OnPreviousClicked(object sender, EventArgs e)
    {
        // 播放上一首的逻辑
    }

    private void OnNextClicked(object sender, EventArgs e)
    {
        // 播放下一首的逻辑
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // 在页面消失时释放 MediaElement 资源
        mediaElement.Handler?.DisconnectHandler();
    }

    private void OnProgressChanged(object sender, EventArgs e)
    {
        Dispatcher.Dispatch(() =>
        {
            if (mediaElement.CurrentState == MediaElementState.None
                || mediaElement.CurrentState == MediaElementState.Failed)
            {
                if (ProgressSlider.Value != 0)
                {
                    ProgressSlider.Value = 0;
                }
                return;
            }
            // 进度条滑动时变更播放位置
            mediaElement.SeekTo(TimeSpan.FromSeconds(ProgressSlider.Value));
            if (mediaElement.CurrentState != MediaElementState.Playing)
            {
                mediaElement.Play();
            }
        });
    }
}