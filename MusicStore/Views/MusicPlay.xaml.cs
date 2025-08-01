﻿using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using MusicStore.Model;
using MusicStore.Service;
using MusicStore.ViewModels;

namespace MusicStore.Views;

public partial class MusicPlay : ContentPage
{
    public MusicPlay(MusicPalyVM vm)
    {
        BindingContext = vm;
        InitializeComponent();

        // 设置音乐文件路径（可从本地或网络加载）
        //mediaElement.Source = MediaSource.FromFile(@"E:\testProject\M5000040uYbR1AkIDA.mp3"); // 替换为实际音乐文件路径

    }

    private void LyricCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            LyricCollectionView.ScrollTo((Lyric)e.CurrentSelection[0],position:ScrollToPosition.Center,animate:true);
        }
    }
}